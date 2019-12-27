using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Appointment(Guid? id, Create.Command command) : base(id)
        {
            CategoryId = command.CategoryId;
            StartTime = command.StartTime;
            EndTime = command.EndTime;
            Name = command.Name;
            PublicDetails = command.PublicDetails;
            InternalDetails = command.InternalDetails;
            StatusId = command.StatusId;
            EmolumentId = command.EmolumentId;
            EmolumentPatternId = command.EmolumentPatternId;
        }

        [JsonConstructor]
        protected Appointment()
        {
        }

        public Guid? CategoryId { get; private set; }
        public virtual SelectValueMapping Category { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Name { get; private set; }
        public string PublicDetails { get; private set; }
        public string InternalDetails { get; private set; }
        public Guid? StatusId { get; private set; }
        public virtual SelectValueMapping Status { get; private set; }
        public Guid? EmolumentId { get; private set; }
        public virtual SelectValueMapping Emolument { get; private set; }
        public Guid? EmolumentPatternId { get; private set; }
        public virtual SelectValueMapping EmolumentPattern { get; private set; }
        public Guid? VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public virtual ICollection<AppointmentRoom> AppointmentRooms { get; private set; } = new HashSet<AppointmentRoom>();
        public virtual ICollection<ProjectAppointment> ProjectAppointments { get; private set; } = new HashSet<ProjectAppointment>();
        public virtual ICollection<RegisterAppointment> RegisterAppointments { get; private set; } = new HashSet<RegisterAppointment>();
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();
    }
}
