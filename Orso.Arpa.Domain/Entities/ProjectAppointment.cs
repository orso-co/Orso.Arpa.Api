using System;

namespace Orso.Arpa.Domain.Entities
{
    public class ProjectAppointment
    {
        public ProjectAppointment(Guid projectId, Guid appointmentId)
        {
            ProjectId = projectId;
            AppointmentId = appointmentId;
        }

        public ProjectAppointment()
        {
        }

        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
    }
}
