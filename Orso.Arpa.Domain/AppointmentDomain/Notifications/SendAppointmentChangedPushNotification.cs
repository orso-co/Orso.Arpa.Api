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

namespace Orso.Arpa.Domain.AppointmentDomain.Notifications
{
    public class SendAppointmentChangedPushNotification : INotificationHandler<AppointmentChangedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<SendAppointmentChangedPushNotification> _logger;

        public SendAppointmentChangedPushNotification(
            INotificationDispatcher notificationDispatcher,
            IArpaContext arpaContext,
            ILogger<SendAppointmentChangedPushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(AppointmentChangedNotification notification, CancellationToken cancellationToken)
        {
            // Get all persons linked to this appointment who have a user account
            List<Guid> recipientUserIds = await _arpaContext.AppointmentParticipations
                .AsNoTracking()
                .Where(ap => ap.AppointmentId == notification.AppointmentId && !ap.Deleted)
                .Select(ap => ap.Person)
                .Where(p => p != null && !p.Deleted && p.User != null)
                .Select(p => p.User.Id)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (!recipientUserIds.Any())
            {
                return;
            }

            var dateStr = notification.StartTime?.ToString("dd.MM.yyyy HH:mm") ?? "";
            var body = string.IsNullOrEmpty(dateStr)
                ? notification.AppointmentName
                : $"{notification.AppointmentName} ({dateStr})";

            await _notificationDispatcher.DispatchAsync(
                NotificationEventType.AppointmentChanged,
                recipientUserIds,
                "Termin aktualisiert",
                body,
                "/#/staff/appointments");
        }
    }
}
