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

namespace Orso.Arpa.Domain.ProjectDomain.Notifications
{
    public class SendProjectInfoChangedPushNotification : INotificationHandler<ProjectInfoChangedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<SendProjectInfoChangedPushNotification> _logger;

        public SendProjectInfoChangedPushNotification(
            INotificationDispatcher notificationDispatcher,
            IArpaContext arpaContext,
            ILogger<SendProjectInfoChangedPushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(ProjectInfoChangedNotification notification, CancellationToken cancellationToken)
        {
            // Get all project participants with a user account
            List<Guid> recipientUserIds = await _arpaContext.ProjectParticipations
                .AsNoTracking()
                .Where(pp => pp.ProjectId == notification.ProjectId && !pp.Deleted)
                .Select(pp => pp.MusicianProfile.Person)
                .Where(p => p != null && !p.Deleted && p.User != null)
                .Select(p => p.User.Id)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (!recipientUserIds.Any())
            {
                return;
            }

            await _notificationDispatcher.DispatchAsync(
                NotificationEventType.ProjectInfoChanged,
                recipientUserIds,
                "Projekt aktualisiert",
                notification.ProjectName,
                "/#/performer/projects");
        }
    }
}
