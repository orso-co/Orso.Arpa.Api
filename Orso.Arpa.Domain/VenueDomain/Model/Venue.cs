using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    public class Venue : BaseEntity
    {
        public Venue(Guid? id, CreateVenue.Command command) : base(id)
        {
            Name = command.Name;
            Description = command.Description;
            Address = new Address(command.AddressId, command);
            AddressId = Address.Id;
        }

        [JsonConstructor]
        protected Venue()
        {
        }

        public void Update(ModifyVenue.Command command)
        {
            Name = command.Name;
            Description = command.Description;
            Address.Update(command);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid? AddressId { get; private set; }

        [CascadingSoftDelete]
        public virtual Address Address { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<Room> Rooms { get; private set; } = new HashSet<Room>();
        public virtual ICollection<Appointment> Appointments { get; private set; } = new HashSet<Appointment>();

        public override string ToString()
        {
            if (Address == null)
            {
                return Name;
            }

            return $"{Name} ({Address.City})";
        }
    }
}
