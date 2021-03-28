using System;

namespace Orso.Arpa.Domain.Entities
{
    public class AppointmentRoom : BaseEntity
    {
        public AppointmentRoom(Guid? id, Appointment appointment, Room room) : base(id)
        {
            Appointment = appointment;
            Room = room;
        }

        public AppointmentRoom(Guid appointmentId, Guid roomId)
        {
            AppointmentId = appointmentId;
            RoomId = roomId;
        }

        public AppointmentRoom()
        {
        }

        public Guid RoomId { get; private set; }
        public virtual Room Room { get; private set; }
        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }
    }
}
