using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.ActivityLogApplication.Interfaces;

namespace Orso.Arpa.Api.Workers;

public sealed class ActivityLogCleanupWorker : BackgroundService
{
    private readonly ILogger<ActivityLogCleanupWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(24);

    public ActivityLogCleanupWorker(
        ILogger<ActivityLogCleanupWorker> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait 5 minutes after startup before first cleanup
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IActivityLogService>();
                await service.CleanupOldLogsAsync(90, stoppingToken);
                _logger.LogInformation("Activity log cleanup completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during activity log cleanup");
            }

            await Task.Delay(CheckInterval, stoppingToken);
        }
    }
}
