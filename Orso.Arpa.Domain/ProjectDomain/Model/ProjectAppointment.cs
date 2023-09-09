using System;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Model
{
    public class ProjectAppointment : BaseEntity
    {
        public ProjectAppointment(Guid? id, Project project, Appointment appointment) : base(id)
        {
            Project = project;
            Appointment = appointment;
        }

        public ProjectAppointment(Guid? id, Guid projectId, Guid appointmentId) : base(id)
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
