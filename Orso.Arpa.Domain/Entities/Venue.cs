using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Venues;

namespace Orso.Arpa.Domain.Entities
{
    public class Venue : BaseEntity
    {
        public Venue(Guid? id, Create.Command command) : base(id)
        {
            Name = command.Name;
            Description = command.Description;
            Address = new Address(id, command);
            AddressId = Address.Id;
        }

        [JsonConstructor]
        protected Venue()
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid? AddressId { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual ICollection<Room> Rooms { get; private set; } = new HashSet<Room>();
        public virtual ICollection<Appointment> Appointments { get; private set; } = new HashSet<Appointment>();

        public virtual ICollection<SphereOfActivityConcert> SphereOfActivityConcerts { get; private set; } = new HashSet<SphereOfActivityConcert>();

        public virtual ICollection<SphereOfActivityRehearsal> SphereOfActivityRehearsals { get; private set; } = new HashSet<SphereOfActivityRehearsal>();
    }
}
