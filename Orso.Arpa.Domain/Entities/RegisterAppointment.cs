using System;

namespace Orso.Arpa.Domain.Entities
{
    public class RegisterAppointment
    {
        public RegisterAppointment(Guid registerId, Guid appointmentId)
        {
            RegisterId = registerId;
            AppointmentId = appointmentId;
        }

        public RegisterAppointment()
        {
        }

        public Guid RegisterId { get; private set; }
        public virtual Register Register { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
    }
}
