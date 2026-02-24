using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Api.Hubs
{
    public class RealtimeNotificationSender : IRealtimeNotificationSender
    {
        private readonly IHubContext<PresenceHub> _hubContext;

        public RealtimeNotificationSender(IHubContext<PresenceHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToUserAsync(Guid userId, string title, string body, string url = null)
        {
            await _hubContext.Clients.Group($"user_{userId}").SendAsync("ReceiveNotification", new
            {
                title,
                body,
                url,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }

        public async Task SendDashboardUpdateAsync(Guid userId, string widgetType, Guid? entityId = null)
        {
            await _hubContext.Clients.Group($"user_{userId}").SendAsync("DashboardUpdate", new
            {
                widgetType,
                entityId,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }

        public async Task SendDashboardUpdateToAllAsync(string widgetType, Guid? entityId = null)
        {
            await _hubContext.Clients.All.SendAsync("DashboardUpdate", new
            {
                widgetType,
                entityId,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }

        public async Task SendAnnouncementToAllAsync(object announcement)
        {
            await _hubContext.Clients.All.SendAsync("NewAnnouncement", announcement);
        }
    }
}
