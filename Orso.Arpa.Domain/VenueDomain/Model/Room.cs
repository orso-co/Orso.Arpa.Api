using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    public class Room : BaseEntity
    {
        public Room(Guid? id, CreateRoom.Command command) : base(id)
        {
            VenueId = command.VenueId;
            Building = command.Building;
            Floor = command.Floor;
            Name = command.Name;
        }

        [JsonConstructor]
        protected Room()
        {
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public string Building { get; private set; }
        public string Floor { get; private set; }
        public string Name { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<AppointmentRoom> AppointmentRooms { get; private set; }
            = new HashSet<AppointmentRoom>();
    }
}
