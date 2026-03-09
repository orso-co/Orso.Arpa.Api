using System;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.SectionDomain.Model
{
    public class SectionAppointment : BaseEntity
    {
        public SectionAppointment(Guid? id, Section section, Appointment appointment) : base(id ?? Guid.NewGuid())
        {
            Section = section;
            Appointment = appointment;
        }

        public SectionAppointment(Guid id, Guid sectionId, Guid appointmentId) : base(id)
        {
            SectionId = sectionId;
            AppointmentId = appointmentId;
        }

        public SectionAppointment()
        {
        }

        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
    }
}
