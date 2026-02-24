using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using libfintx.FinTS;
using libfintx.FinTS.Data;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.FinanceDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.Finance
{
    public class FinTsBalanceService : IFinTsBalanceService
    {
        private readonly ICredentialEncryptionService _encryptionService;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<FinTsBalanceService> _logger;

        private static readonly ConcurrentDictionary<Guid, (TaskCompletionSource<string> tcs, DateTime expiry)> _pendingTanRequests = new();

        public FinTsBalanceService(
            ICredentialEncryptionService encryptionService,
            ILoggerFactory loggerFactory,
            ILogger<FinTsBalanceService> logger)
        {
            _encryptionService = encryptionService;
            _loggerFactory = loggerFactory;
            _logger = logger;
        }

        public async Task<FinTsBalanceResult> GetBalanceAsync(string encryptedCredentials, CancellationToken cancellationToken)
        {
            try
            {
                var json = _encryptionService.Decrypt(encryptedCredentials);
                if (json == null)
                    return new FinTsBalanceResult(null, null, null, false, false, ErrorMessage: "Failed to decrypt credentials");

                var credentials = JsonSerializer.Deserialize<FinTsCredentials>(json);
                if (credentials == null)
                    return new FinTsBalanceResult(null, null, null, false, false, ErrorMessage: "Invalid credentials format");

                var connectionDetails = new ConnectionDetails
                {
                    Url = credentials.BankUrl,
                    Blz = credentials.BankCode,
                    UserId = credentials.UserId,
                    Pin = credentials.Pin,
                    Account = credentials.AccountNumber,
                    SubAccount = credentials.SubAccountFeature,
                    Iban = credentials.Iban,
                    Bic = credentials.Bic,
                };

                _logger.LogInformation("FinTS connecting to {Url} with BLZ {Blz}, User {UserId}, Account {Account}, IBAN {Iban}, BIC {Bic}",
                    credentials.BankUrl, credentials.BankCode, credentials.UserId, credentials.AccountNumber, credentials.Iban, credentials.Bic);

                var client = new FinTsClient(connectionDetails, true, null, _loggerFactory);

                // Set TAN method (HIRMS) and medium BEFORE sync — required for PSD2 compliance
                if (!string.IsNullOrWhiteSpace(credentials.TanMethod))
                {
                    client.HIRMS = credentials.TanMethod;
                    _logger.LogInformation("FinTS TAN method (HIRMS) set to: {TanMethod}", credentials.TanMethod);
                }
                if (!string.IsNullOrWhiteSpace(credentials.TanMediumName))
                {
                    client.HITAB = credentials.TanMediumName;
                    _logger.LogInformation("FinTS TAN medium (HITAB) set to: {TanMediumName}", credentials.TanMediumName);
                }

                // Synchronize first to establish session
                _logger.LogInformation("FinTS synchronization starting...");
                try
                {
                    var syncResult = await client.Synchronization();
                    _logger.LogInformation("FinTS sync result: Success={Success}, Messages={Messages}",
                        syncResult.IsSuccess, string.Join("; ", syncResult.Messages));

                    if (!syncResult.IsSuccess)
                    {
                        return new FinTsBalanceResult(null, null, null, false, syncResult.IsTanRequired,
                            ErrorMessage: $"FinTS sync failed: {string.Join("; ", syncResult.Messages)}");
                    }
                }
                catch (Exception syncEx)
                {
                    var innerMsg = syncEx.InnerException?.Message ?? "no inner exception";
                    var innerType = syncEx.InnerException?.GetType().Name ?? "none";
                    _logger.LogError(syncEx, "FinTS synchronization threw exception. Inner: [{InnerType}] {InnerMessage}", innerType, innerMsg);
                    return new FinTsBalanceResult(null, null, null, false, false,
                        ErrorMessage: $"FinTS sync exception: {syncEx.Message} (Inner: [{innerType}] {innerMsg})");
                }

                Guid? tanRequestId = null;
                string tanChallenge = null;

                var tanDialog = new TANDialog(async (dialog) =>
                {
                    // TAN required — create a pending request and wait
                    tanRequestId = Guid.NewGuid();
                    tanChallenge = dialog.DialogResult?.RawData ?? "TAN eingeben";
                    var tcs = new TaskCompletionSource<string>();
                    _pendingTanRequests[tanRequestId.Value] = (tcs, DateTime.UtcNow.AddMinutes(5));

                    CleanupExpiredRequests();

                    try
                    {
                        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                        cts.CancelAfter(TimeSpan.FromMinutes(5));
                        cts.Token.Register(() => tcs.TrySetCanceled());
                        return await tcs.Task;
                    }
                    catch (OperationCanceledException)
                    {
                        return string.Empty;
                    }
                });

                var balanceResult = await client.Balance(tanDialog);

                if (tanRequestId.HasValue && _pendingTanRequests.ContainsKey(tanRequestId.Value))
                {
                    // TAN was requested but not yet submitted — return TanRequired
                    return new FinTsBalanceResult(
                        null, null, null, false, true,
                        TanChallenge: tanChallenge ?? "TAN eingeben",
                        TanMediumName: "FinTS TAN",
                        TanRequestId: tanRequestId);
                }

                if (!balanceResult.IsSuccess)
                {
                    return new FinTsBalanceResult(null, null, null, false, balanceResult.IsTanRequired,
                        ErrorMessage: $"Balance request failed: {string.Join("; ", balanceResult.Messages)}");
                }

                var balance = balanceResult.Data?.Balance ?? 0;
                var availableBalance = balanceResult.Data?.AvailableBalance;

                return new FinTsBalanceResult(balance, availableBalance, "EUR", true, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching FinTS balance");
                return new FinTsBalanceResult(null, null, null, false, false, ErrorMessage: ex.Message);
            }
        }

        public async Task<FinTsBalanceResult> SubmitTanAndGetBalanceAsync(Guid tanRequestId, string tan, CancellationToken cancellationToken)
        {
            try
            {
                if (!_pendingTanRequests.TryRemove(tanRequestId, out var requestInfo))
                {
                    return new FinTsBalanceResult(null, null, null, false, false,
                        ErrorMessage: "TAN dialog expired or not found");
                }

                if (requestInfo.expiry <= DateTime.UtcNow)
                {
                    requestInfo.tcs.TrySetCanceled();
                    return new FinTsBalanceResult(null, null, null, false, false,
                        ErrorMessage: "TAN dialog has expired");
                }

                // Submit TAN — this resumes the pending Balance() call
                requestInfo.tcs.TrySetResult(tan);

                // The original Balance() call in GetBalanceAsync should now complete
                // Since the dialog is async, we return a success indicator
                // The actual balance will be captured by the worker on the next sync
                return new FinTsBalanceResult(null, null, "EUR", true, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting TAN for request {TanRequestId}", tanRequestId);
                return new FinTsBalanceResult(null, null, null, false, false, ErrorMessage: ex.Message);
            }
        }

        private static void CleanupExpiredRequests()
        {
            foreach (var kvp in _pendingTanRequests)
            {
                if (kvp.Value.expiry <= DateTime.UtcNow)
                {
                    if (_pendingTanRequests.TryRemove(kvp.Key, out var expired))
                    {
                        expired.tcs.TrySetCanceled();
                    }
                }
            }
        }
    }

    internal class FinTsCredentials
    {
        public string BankUrl { get; set; }
        public int BankCode { get; set; }
        public string UserId { get; set; }
        public string Pin { get; set; }
        public string AccountNumber { get; set; }
        public string SubAccountFeature { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string TanMethod { get; set; }
        public string TanMediumName { get; set; }
    }
}
