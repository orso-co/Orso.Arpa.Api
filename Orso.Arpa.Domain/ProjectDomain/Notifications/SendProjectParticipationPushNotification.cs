using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Domain.ProjectDomain.Notifications
{
    public class SendProjectParticipationPushNotification : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly ILogger<SendProjectParticipationPushNotification> _logger;

        public SendProjectParticipationPushNotification(
            INotificationDispatcher notificationDispatcher,
            ILogger<SendProjectParticipationPushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _logger = logger;
        }

        public async Task Handle(ProjectParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.ChangedByPerformer)
            {
                return;
            }

            var person = notification.ProjectParticipation.MusicianProfile?.Person;
            var user = person?.User;

            if (user == null)
            {
                _logger.LogWarning("Cannot send push notification for project participation change: user not found");
                return;
            }

            var projectName = notification.ProjectParticipation.Project?.ToString() ?? "Projekt";
            var status = notification.ProjectParticipation.ParticipationStatusInternal?.ToString()
                      ?? notification.ProjectParticipation.ParticipationStatusInner?.ToString()
                      ?? "aktualisiert";

            await _notificationDispatcher.DispatchAsync(
                NotificationEventType.ProjectParticipationChanged,
                [user.Id],
                $"Projektstatus: {projectName}",
                $"Dein Teilnahmestatus wurde auf \"{status}\" gesetzt.",
                "/#/performer/projects");
        }
    }
}
