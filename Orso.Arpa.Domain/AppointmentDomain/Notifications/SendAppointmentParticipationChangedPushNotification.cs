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
    public class SendAppointmentParticipationChangedPushNotification : INotificationHandler<AppointmentParticipationChangedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<SendAppointmentParticipationChangedPushNotification> _logger;

        public SendAppointmentParticipationChangedPushNotification(
            INotificationDispatcher notificationDispatcher,
            IArpaContext arpaContext,
            ILogger<SendAppointmentParticipationChangedPushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(AppointmentParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.ChangedByStaff)
            {
                // Staff changed → notify the performer
                Guid? performerUserId = await _arpaContext.Persons
                    .AsNoTracking()
                    .Where(p => p.Id == notification.PersonId && !p.Deleted && p.User != null)
                    .Select(p => (Guid?)p.User.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (performerUserId == null)
                {
                    return;
                }

                await _notificationDispatcher.DispatchAsync(
                    NotificationEventType.AppointmentParticipationChanged,
                    [performerUserId.Value],
                    "Teilnahmestatus geändert",
                    $"{notification.AppointmentName}: Dein Status wurde auf \"{notification.Prediction}\" gesetzt.",
                    "/#/performer/appointments");
            }
            else
            {
                // Performer changed → notify staff of the appointment
                List<Guid> staffUserIds = await _arpaContext.AppointmentParticipations
                    .AsNoTracking()
                    .Where(ap => ap.AppointmentId == notification.AppointmentId && !ap.Deleted)
                    .Select(ap => ap.Person)
                    .Where(p => p != null && !p.Deleted && p.User != null)
                    .Where(p => p.User.UserRoles.Any(ur => ur.Role.Name == "Staff"))
                    .Select(p => p.User.Id)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                if (!staffUserIds.Any())
                {
                    return;
                }

                await _notificationDispatcher.DispatchAsync(
                    NotificationEventType.AppointmentParticipationChanged,
                    staffUserIds,
                    "Teilnahme-Rückmeldung",
                    $"{notification.PersonName} hat für \"{notification.AppointmentName}\" geantwortet: {notification.Prediction}",
                    "/#/staff/appointments");
            }
        }
    }
}
