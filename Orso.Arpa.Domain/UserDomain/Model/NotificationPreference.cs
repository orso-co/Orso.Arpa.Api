using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class NotificationPreference : BaseEntity
    {
        public NotificationPreference(Guid? id, Guid userId, NotificationEventType eventType, NotificationChannel channels)
            : base(id)
        {
            UserId = userId;
            EventType = eventType;
            Channels = channels;
        }

        protected NotificationPreference()
        {
        }

        public Guid UserId { get; private set; }
        public NotificationEventType EventType { get; private set; }
        public NotificationChannel Channels { get; set; }

        public virtual User User { get; private set; }
    }
}
