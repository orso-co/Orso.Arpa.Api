using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Rooms;

namespace Orso.Arpa.Domain.Entities
{
    public class Room : BaseEntity
    {
        public Room(Guid? id, Create.Command command) : base(id)
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

        public virtual ICollection<AppointmentRoom> AppointmentRooms { get; private set; }
            = new HashSet<AppointmentRoom>();
    }
}
