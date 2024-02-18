using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain._General.Model;
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
            Address = new Address(command);
        }

        [JsonConstructor]
        protected Venue()
        {
        }

        public void Update(ModifyVenue.Command command)
        {
            Name = command.Name;
            Description = command.Description;
            Address.Address1 = command.Address1;
            Address.Address2 = command.Address2;
            Address.Zip = command.Zip;
            Address.City = command.City;
            Address.UrbanDistrict = command.UrbanDistrict;
            Address.Country = command.Country;
            Address.State = command.State;
            Address.CommentInner = command.CommentInner;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Address Address { get; private set; }

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
