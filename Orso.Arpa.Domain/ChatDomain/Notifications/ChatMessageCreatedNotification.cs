using System;
using MediatR;

namespace Orso.Arpa.Domain.ChatDomain.Notifications
{
    public class ChatMessageCreatedNotification : INotification
    {
        public Guid RoomId { get; set; }
        public Guid SenderUserId { get; set; }
        public string SenderDisplayName { get; set; }
        public string MessageContent { get; set; }
    }
}
