using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.FinanceApplication.Interfaces;
using Orso.Arpa.Application.FinanceApplication.Model;
using Orso.Arpa.Domain.FinanceDomain.Enums;
using Orso.Arpa.Domain.FinanceDomain.Interfaces;
using Orso.Arpa.Domain.FinanceDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Application.FinanceApplication.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly IArpaContext _context;
        private readonly ICredentialEncryptionService _encryptionService;
        private readonly IPayPalBalanceService _payPalService;
        private readonly IFinTsBalanceService _finTsService;
        private readonly ILogger<FinanceService> _logger;

        public FinanceService(
            IArpaContext context,
            ICredentialEncryptionService encryptionService,
            IPayPalBalanceService payPalService,
            IFinTsBalanceService finTsService,
            ILogger<FinanceService> logger)
        {
            _context = context;
            _encryptionService = encryptionService;
            _payPalService = payPalService;
            _finTsService = finTsService;
            _logger = logger;
        }

        public async Task<List<OrganizationAccountDto>> GetAccountsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrganizationBankAccounts
                .Where(a => !a.Deleted)
                .OrderBy(a => a.Name)
                .Select(a => new OrganizationAccountDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Iban = a.Iban,
                    Bic = a.Bic,
                    BankName = a.BankName,
                    AccountType = a.AccountType.ToString(),
                    IsActive = a.IsActive,
                    HasFinTsCredentials = a.EncryptedFinTsCredentials != null,
                    HasPayPalCredentials = a.EncryptedPayPalCredentials != null,
                    CreatedAt = a.CreatedAt,
                    ModifiedAt = a.ModifiedAt
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<OrganizationAccountDto> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.OrganizationBankAccounts
                .Where(a => a.Id == id && !a.Deleted)
                .Select(a => new OrganizationAccountDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Iban = a.Iban,
                    Bic = a.Bic,
                    BankName = a.BankName,
                    AccountType = a.AccountType.ToString(),
                    IsActive = a.IsActive,
                    HasFinTsCredentials = a.EncryptedFinTsCredentials != null,
                    HasPayPalCredentials = a.EncryptedPayPalCredentials != null,
                    CreatedAt = a.CreatedAt,
                    ModifiedAt = a.ModifiedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<OrganizationAccountDto> CreateAccountAsync(CreateOrganizationAccountDto dto, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<OrganizationAccountType>(dto.AccountType, true, out var accountType))
                throw new ArgumentException($"Invalid account type: {dto.AccountType}");

            var account = new OrganizationBankAccount(null, dto.Name, dto.Iban, dto.Bic, dto.BankName, accountType);

            if (dto.FinTsCredentials != null)
            {
                var json = JsonSerializer.Serialize(dto.FinTsCredentials);
                account.SetEncryptedFinTsCredentials(_encryptionService.Encrypt(json));
            }

            if (dto.PayPalCredentials != null)
            {
                var json = JsonSerializer.Serialize(dto.PayPalCredentials);
                account.SetEncryptedPayPalCredentials(_encryptionService.Encrypt(json));
            }

            _context.OrganizationBankAccounts.Add(account);
            await _context.SaveChangesAsync(cancellationToken);

            return await GetAccountByIdAsync(account.Id, cancellationToken);
        }

        public async Task<OrganizationAccountDto> UpdateAccountAsync(Guid id, ModifyOrganizationAccountDto dto, CancellationToken cancellationToken = default)
        {
            var account = await _context.OrganizationBankAccounts.FindAsync([id], cancellationToken)
                ?? throw new KeyNotFoundException($"Account {id} not found");

            if (!Enum.TryParse<OrganizationAccountType>(dto.AccountType, true, out var accountType))
                throw new ArgumentException($"Invalid account type: {dto.AccountType}");

            account.Update(dto.Name, dto.Iban, dto.Bic, dto.BankName, accountType, dto.IsActive);

            if (dto.FinTsCredentials != null)
            {
                var json = JsonSerializer.Serialize(dto.FinTsCredentials);
                account.SetEncryptedFinTsCredentials(_encryptionService.Encrypt(json));
            }

            if (dto.PayPalCredentials != null)
            {
                var json = JsonSerializer.Serialize(dto.PayPalCredentials);
                account.SetEncryptedPayPalCredentials(_encryptionService.Encrypt(json));
            }

            await _context.SaveChangesAsync(cancellationToken);
            return await GetAccountByIdAsync(id, cancellationToken);
        }

        public async Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var account = await _context.OrganizationBankAccounts.FindAsync([id], cancellationToken)
                ?? throw new KeyNotFoundException($"Account {id} not found");

            _context.Remove(account);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<BalanceSummaryDto> GetBalanceSummaryAsync(CancellationToken cancellationToken = default)
        {
            var accounts = await _context.OrganizationBankAccounts
                .Where(a => !a.Deleted && a.IsActive)
                .ToListAsync(cancellationToken);

            var summary = new BalanceSummaryDto
            {
                Currency = "EUR",
                Accounts = new List<AccountBalanceDto>()
            };

            foreach (var account in accounts)
            {
                var latestSnapshot = await _context.BankAccountBalanceSnapshots
                    .Where(s => s.OrganizationBankAccountId == account.Id && !s.Deleted && s.SyncStatus == BalanceSyncStatus.Success)
                    .OrderByDescending(s => s.SyncedAt)
                    .FirstOrDefaultAsync(cancellationToken);

                summary.Accounts.Add(new AccountBalanceDto
                {
                    AccountId = account.Id,
                    Name = account.Name,
                    AccountType = account.AccountType.ToString(),
                    Balance = latestSnapshot?.Balance ?? 0,
                    AvailableBalance = latestSnapshot?.AvailableBalance,
                    Currency = latestSnapshot?.Currency ?? "EUR",
                    LastSyncedAt = latestSnapshot?.SyncedAt,
                    SyncStatus = latestSnapshot?.SyncStatus.ToString()
                });
            }

            summary.TotalBalance = summary.Accounts.Sum(a => a.Balance);
            return summary;
        }

        public async Task<List<BalanceSnapshotDto>> GetBalanceHistoryAsync(Guid accountId, int days = 30, CancellationToken cancellationToken = default)
        {
            var since = DateTime.UtcNow.AddDays(-days);

            return await _context.BankAccountBalanceSnapshots
                .Where(s => s.OrganizationBankAccountId == accountId && !s.Deleted && s.SyncedAt >= since)
                .OrderByDescending(s => s.SyncedAt)
                .Select(s => new BalanceSnapshotDto
                {
                    Id = s.Id,
                    OrganizationBankAccountId = s.OrganizationBankAccountId,
                    AccountName = s.OrganizationBankAccount.Name,
                    Balance = s.Balance,
                    AvailableBalance = s.AvailableBalance,
                    Currency = s.Currency,
                    BalanceDate = s.BalanceDate,
                    SyncedAt = s.SyncedAt,
                    SyncStatus = s.SyncStatus.ToString(),
                    ErrorMessage = s.ErrorMessage
                })
                .ToListAsync(cancellationToken);
        }

        public async Task TriggerSyncAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var account = await _context.OrganizationBankAccounts.FindAsync([accountId], cancellationToken)
                ?? throw new KeyNotFoundException($"Account {accountId} not found");

            try
            {
                if (account.AccountType == OrganizationAccountType.PayPal && account.EncryptedPayPalCredentials != null)
                {
                    var result = await _payPalService.GetBalanceAsync(account.EncryptedPayPalCredentials, cancellationToken);
                    var snapshot = new BankAccountBalanceSnapshot(
                        null,
                        accountId,
                        result.Balance,
                        result.AvailableBalance,
                        result.Currency,
                        DateTime.UtcNow,
                        result.Success ? BalanceSyncStatus.Success : BalanceSyncStatus.Failed,
                        result.ErrorMessage);
                    _context.BankAccountBalanceSnapshots.Add(snapshot);
                }
                else if (account.EncryptedFinTsCredentials != null)
                {
                    var result = await _finTsService.GetBalanceAsync(account.EncryptedFinTsCredentials, cancellationToken);

                    if (result.TanRequired)
                    {
                        var tanRequest = new PendingTanRequest(
                            null,
                            accountId,
                            result.TanChallenge,
                            result.TanMediumName,
                            DateTime.UtcNow.AddMinutes(5));
                        _context.PendingTanRequests.Add(tanRequest);

                        var snapshot = new BankAccountBalanceSnapshot(
                            null, accountId, 0, null, "EUR", DateTime.UtcNow,
                            BalanceSyncStatus.TanRequired, "TAN erforderlich");
                        _context.BankAccountBalanceSnapshots.Add(snapshot);
                    }
                    else if (result.Success)
                    {
                        var snapshot = new BankAccountBalanceSnapshot(
                            null,
                            accountId,
                            result.Balance ?? 0,
                            result.AvailableBalance,
                            result.Currency ?? "EUR",
                            DateTime.UtcNow,
                            BalanceSyncStatus.Success);
                        _context.BankAccountBalanceSnapshots.Add(snapshot);
                    }
                    else
                    {
                        var snapshot = new BankAccountBalanceSnapshot(
                            null, accountId, 0, null, "EUR", DateTime.UtcNow,
                            BalanceSyncStatus.Failed, result.ErrorMessage);
                        _context.BankAccountBalanceSnapshots.Add(snapshot);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing account {AccountId}", accountId);
                var snapshot = new BankAccountBalanceSnapshot(
                    null, accountId, 0, null, "EUR", DateTime.UtcNow,
                    BalanceSyncStatus.Failed, ex.Message);
                _context.BankAccountBalanceSnapshots.Add(snapshot);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<PendingTanRequestDto>> GetPendingTanRequestsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.PendingTanRequests
                .Where(r => !r.Deleted && r.Status == TanRequestStatus.Pending && r.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new PendingTanRequestDto
                {
                    Id = r.Id,
                    OrganizationBankAccountId = r.OrganizationBankAccountId,
                    AccountName = r.OrganizationBankAccount.Name,
                    TanChallenge = r.TanChallenge,
                    TanMediumName = r.TanMediumName,
                    Status = r.Status.ToString(),
                    ExpiresAt = r.ExpiresAt
                })
                .ToListAsync(cancellationToken);
        }

        public async Task SubmitTanAsync(SubmitTanDto dto, CancellationToken cancellationToken = default)
        {
            var tanRequest = await _context.PendingTanRequests.FindAsync([dto.TanRequestId], cancellationToken)
                ?? throw new KeyNotFoundException($"TAN request {dto.TanRequestId} not found");

            if (tanRequest.Status != TanRequestStatus.Pending)
                throw new InvalidOperationException("TAN request is no longer pending");

            if (tanRequest.ExpiresAt <= DateTime.UtcNow)
            {
                tanRequest.Expire();
                await _context.SaveChangesAsync(cancellationToken);
                throw new InvalidOperationException("TAN request has expired");
            }

            tanRequest.Submit(dto.Tan);
            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                var result = await _finTsService.SubmitTanAndGetBalanceAsync(dto.TanRequestId, dto.Tan, cancellationToken);

                if (result.Success)
                {
                    tanRequest.Complete();
                    var snapshot = new BankAccountBalanceSnapshot(
                        null,
                        tanRequest.OrganizationBankAccountId,
                        result.Balance ?? 0,
                        result.AvailableBalance,
                        result.Currency ?? "EUR",
                        DateTime.UtcNow,
                        BalanceSyncStatus.Success);
                    _context.BankAccountBalanceSnapshots.Add(snapshot);
                }
                else
                {
                    var snapshot = new BankAccountBalanceSnapshot(
                        null,
                        tanRequest.OrganizationBankAccountId,
                        0, null, "EUR", DateTime.UtcNow,
                        BalanceSyncStatus.Failed, result.ErrorMessage);
                    _context.BankAccountBalanceSnapshots.Add(snapshot);
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting TAN for request {TanRequestId}", dto.TanRequestId);
                throw;
            }
        }
    }
}
