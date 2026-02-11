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
    }
}
