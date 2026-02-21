using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Domain.MediathekDomain.Notifications
{
    public class SendMediathekAccessGrantedPushNotification : INotificationHandler<MediathekAccessGrantedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly ILogger<SendMediathekAccessGrantedPushNotification> _logger;

        public SendMediathekAccessGrantedPushNotification(
            INotificationDispatcher notificationDispatcher,
            ILogger<SendMediathekAccessGrantedPushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _logger = logger;
        }

        public async Task Handle(MediathekAccessGrantedNotification notification, CancellationToken cancellationToken)
        {
            var user = notification.MediathekAccess.Person?.User;

            if (user == null)
            {
                _logger.LogWarning("Cannot send push notification for mediathek access granted: user not found");
                return;
            }

            await _notificationDispatcher.DispatchAsync(
                NotificationEventType.MediathekAccessGranted,
                [user.Id],
                "Mediathek-Zugang freigeschaltet",
                "Dein Zugang zur Mediathek wurde freigeschaltet. Du kannst jetzt auf alle Medien zugreifen.",
                "/#/user/mediathek");
        }
    }
}
