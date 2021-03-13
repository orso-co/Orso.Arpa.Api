using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SectionAppointment
    {
        public SectionAppointment(Section section, Appointment appointment)
        {
            Section = section;
            Appointment = appointment;
        }

        public SectionAppointment(Guid sectionId, Guid appointmentId)
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
