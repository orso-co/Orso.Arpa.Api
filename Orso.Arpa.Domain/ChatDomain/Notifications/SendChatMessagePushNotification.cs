using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Domain.ChatDomain.Notifications
{
    public class SendChatMessagePushNotification : INotificationHandler<ChatMessageCreatedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<SendChatMessagePushNotification> _logger;

        public SendChatMessagePushNotification(
            INotificationDispatcher notificationDispatcher,
            IArpaContext arpaContext,
            ILogger<SendChatMessagePushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(ChatMessageCreatedNotification notification, CancellationToken cancellationToken)
        {
            // Get all room members except sender
            List<Guid> recipientUserIds = await _arpaContext.ChatRoomMembers
                .AsNoTracking()
                .Where(m => m.ChatRoomId == notification.RoomId && !m.Deleted && m.UserId != notification.SenderUserId)
                .Select(m => m.UserId)
                .ToListAsync(cancellationToken);

            if (!recipientUserIds.Any())
            {
                return;
            }

            var body = notification.MessageContent.Length > 100
                ? notification.MessageContent[..100] + "..."
                : notification.MessageContent;

            await _notificationDispatcher.DispatchAsync(
                NotificationEventType.ChatMessage,
                recipientUserIds,
                notification.SenderDisplayName,
                body,
                "/#/user/chat");
        }
    }
}
