using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Infrastructure.Presence;

namespace Orso.Arpa.Api.Hubs
{
    public class PresenceCleanupService : BackgroundService
    {
        private readonly IPresenceTracker _presenceTracker;
        private readonly IHubContext<PresenceHub> _hubContext;
        private readonly ILogger<PresenceCleanupService> _logger;

        public PresenceCleanupService(
            IPresenceTracker presenceTracker,
            IHubContext<PresenceHub> hubContext,
            ILogger<PresenceCleanupService> logger)
        {
            _presenceTracker = presenceTracker;
            _hubContext = hubContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);

                try
                {
                    var removedUserIds = await _presenceTracker.CleanupStaleConnections(TimeSpan.FromMinutes(2));

                    foreach (var userId in removedUserIds)
                    {
                        _logger.LogInformation("Stale connection cleanup: removed user {UserId}", userId);
                        await _hubContext.Clients.All.SendAsync("UserIsOffline", new { userId }, stoppingToken);
                    }

                    if (removedUserIds.Count > 0)
                    {
                        var recentlyOnline = await _presenceTracker.GetRecentlyOnlineUsers(TimeSpan.FromHours(1));
                        await _hubContext.Clients.All.SendAsync("RecentlyOnlineUsersList", recentlyOnline, stoppingToken);
                    }
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during presence cleanup");
                }
            }
        }
    }
}
