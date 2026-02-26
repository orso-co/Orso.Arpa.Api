using System;
using MediatR;

namespace Orso.Arpa.Domain.AppointmentDomain.Notifications
{
    public class AppointmentParticipationChangedNotification : INotification
    {
        public Guid AppointmentId { get; set; }
        public string AppointmentName { get; set; }
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public string Prediction { get; set; }
        public bool ChangedByStaff { get; set; }
    }
}
