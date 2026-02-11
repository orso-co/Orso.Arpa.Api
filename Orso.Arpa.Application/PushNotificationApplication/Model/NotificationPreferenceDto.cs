using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Application.PushNotificationApplication.Model
{
    public class NotificationPreferenceDto
    {
        public NotificationEventType EventType { get; set; }
        public NotificationChannel Channels { get; set; }
    }
}
