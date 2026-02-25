using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.FinanceApplication.Interfaces;
using Orso.Arpa.Domain.FinanceDomain.Enums;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Api.Workers;

public sealed class BankBalanceSyncWorker : BackgroundService
{
    private readonly ILogger<BankBalanceSyncWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly FinanceConfiguration _config;
    private const string LoggerPrefix = "BANK_BALANCE_SYNC:";
    private const int MaxConsecutiveFailures = 3;

    public BankBalanceSyncWorker(
        ILogger<BankBalanceSyncWorker> logger,
        IServiceProvider serviceProvider,
        FinanceConfiguration config)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Prefix} Worker started, sync interval: {Interval} minutes, auto-disable after {MaxFail} consecutive failures",
            LoggerPrefix, _config.SyncIntervalMinutes, MaxConsecutiveFailures);

        // Initial delay to let the app start up
        await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await SyncAllAccountsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Unhandled error during sync cycle", LoggerPrefix);
            }

            await Task.Delay(TimeSpan.FromMinutes(_config.SyncIntervalMinutes), stoppingToken);
        }
    }

    private async Task SyncAllAccountsAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IArpaContext>();
        var financeService = scope.ServiceProvider.GetRequiredService<IFinanceService>();

        var activeAccounts = await context.OrganizationBankAccounts
            .Where(a => !a.Deleted && a.IsActive)
            .ToListAsync(cancellationToken);

        _logger.LogInformation("{Prefix} Syncing {Count} active accounts", LoggerPrefix, activeAccounts.Count);

        var disabledAccounts = new List<string>();

        foreach (var account in activeAccounts)
        {
            try
            {
                // Check recent failures BEFORE attempting sync
                var recentFailures = await context.BankAccountBalanceSnapshots
                    .Where(s => s.OrganizationBankAccountId == account.Id && !s.Deleted)
                    .OrderByDescending(s => s.SyncedAt)
                    .Take(MaxConsecutiveFailures)
                    .ToListAsync(cancellationToken);

                var consecutiveFailures = recentFailures
                    .TakeWhile(s => s.SyncStatus != BalanceSyncStatus.Success)
                    .Count();

                if (consecutiveFailures >= MaxConsecutiveFailures)
                {
                    _logger.LogWarning("{Prefix} Account {Name} ({Type}) has {Failures} consecutive failures — auto-disabling to prevent PIN lockout",
                        LoggerPrefix, account.Name, account.AccountType, consecutiveFailures);

                    account.Deactivate();
                    disabledAccounts.Add(account.Name);
                    continue;
                }

                _logger.LogInformation("{Prefix} Syncing account: {Name} ({Type})", LoggerPrefix, account.Name, account.AccountType);
                await financeService.TriggerSyncAsync(account.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Prefix} Error syncing account {AccountId} ({Name})", LoggerPrefix, account.Id, account.Name);
            }
        }

        // Save disabled accounts
        if (disabledAccounts.Count > 0)
        {
            await context.SaveChangesAsync(cancellationToken);
            await SendAccountDisabledNotificationAsync(disabledAccounts);
        }

        // Check for pending TAN requests and send push notification
        var pendingTans = await context.PendingTanRequests
            .Where(r => !r.Deleted && r.Status == Domain.FinanceDomain.Enums.TanRequestStatus.Pending && r.ExpiresAt > DateTime.UtcNow)
            .CountAsync(cancellationToken);

        if (pendingTans > 0)
        {
            await SendTanNotificationAsync(pendingTans);
        }

        _logger.LogInformation("{Prefix} Sync cycle complete", LoggerPrefix);
    }

    private async Task SendAccountDisabledNotificationAsync(List<string> accountNames)
    {
        try
        {
            using var client = new HttpClient();
            var names = string.Join(", ", accountNames);
            var message = $"ARPA Finance: Konto(n) automatisch deaktiviert nach {MaxConsecutiveFailures} Fehlversuchen: {names}. Bitte manuell in ARPA unter Finanzen reaktivieren.";
            var content = new StringContent(message, Encoding.UTF8, "text/plain");
            content.Headers.Add("Title", "ARPA Finance - Konto deaktiviert!");
            content.Headers.Add("Priority", "urgent");
            content.Headers.Add("Tags", "rotating_light,bank");

            await client.PostAsync($"https://ntfy.sh/{_config.NtfyTopic}", content);
            _logger.LogWarning("{Prefix} Push notification sent for disabled accounts: {Names}", LoggerPrefix, names);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "{Prefix} Failed to send account-disabled push notification", LoggerPrefix);
        }
    }

    private async Task SendTanNotificationAsync(int count)
    {
        try
        {
            using var client = new HttpClient();
            var message = $"ARPA: {count} TAN-Eingabe(n) erforderlich für Kontostand-Abruf";
            var content = new StringContent(message, Encoding.UTF8, "text/plain");
            content.Headers.Add("Title", "ARPA Finance - TAN benötigt");
            content.Headers.Add("Priority", "high");
            content.Headers.Add("Tags", "bank,warning");

            await client.PostAsync($"https://ntfy.sh/{_config.NtfyTopic}", content);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "{Prefix} Failed to send TAN push notification", LoggerPrefix);
        }
    }
}
