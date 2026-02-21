using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.WebPush
{
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly IArpaContext _arpaContext;
        private readonly IWebPushService _webPushService;
        private readonly IRealtimeNotificationSender _realtimeNotificationSender;
        private readonly ILogger<NotificationDispatcher> _logger;

        private static readonly Dictionary<NotificationEventType, NotificationChannel> DefaultChannels = new()
        {
            { NotificationEventType.ChatMessage, NotificationChannel.Push | NotificationChannel.InApp },
            { NotificationEventType.AppointmentReminder, NotificationChannel.Push | NotificationChannel.Email },
            { NotificationEventType.ProjectParticipationChanged, NotificationChannel.Push | NotificationChannel.Email },
            { NotificationEventType.AccountActivated, NotificationChannel.Push | NotificationChannel.Email },
            { NotificationEventType.MediathekAccessGranted, NotificationChannel.Push | NotificationChannel.Email },
        };

        public NotificationDispatcher(
            IArpaContext arpaContext,
            IWebPushService webPushService,
            IRealtimeNotificationSender realtimeNotificationSender,
            ILogger<NotificationDispatcher> logger)
        {
            _arpaContext = arpaContext;
            _webPushService = webPushService;
            _realtimeNotificationSender = realtimeNotificationSender;
            _logger = logger;
        }

        public async Task DispatchAsync(NotificationEventType eventType, IEnumerable<Guid> recipientUserIds,
            string title, string body, string url = null)
        {
            var userIds = recipientUserIds.ToList();
            if (!userIds.Any())
            {
                return;
            }

            var preferences = await _arpaContext.NotificationPreferences
                .AsNoTracking()
                .Where(p => userIds.Contains(p.UserId) && p.EventType == eventType && !p.Deleted)
                .ToDictionaryAsync(p => p.UserId, p => p.Channels);

            foreach (var userId in userIds)
            {
                var channels = preferences.TryGetValue(userId, out var userChannels)
                    ? userChannels
                    : GetDefaultChannels(eventType);

                try
                {
                    if (channels.HasFlag(NotificationChannel.InApp))
                    {
                        await _realtimeNotificationSender.SendToUserAsync(userId, title, body, url);
                    }

                    if (channels.HasFlag(NotificationChannel.Push))
                    {
                        await _webPushService.SendAsync(userId, title, body, url);
                    }

                    // Email is handled by existing MediatR handlers - not dispatched here
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to dispatch notification to user {UserId} for event {EventType}", userId, eventType);
                }
            }
        }

        private static NotificationChannel GetDefaultChannels(NotificationEventType eventType)
        {
            return DefaultChannels.TryGetValue(eventType, out var channels) ? channels : NotificationChannel.Push | NotificationChannel.InApp;
        }
    }
}
