using System;
using MediatR;

namespace Orso.Arpa.Domain.AppointmentDomain.Notifications
{
    public class AppointmentChangedNotification : INotification
    {
        public Guid AppointmentId { get; set; }
        public string AppointmentName { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
