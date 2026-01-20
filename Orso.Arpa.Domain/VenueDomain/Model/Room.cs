using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Enums;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    public class Room : BaseEntity
    {
        public Room(Guid id, CreateRoom.Command command) : base(id)
        {
            VenueId = command.VenueId;
            Building = command.Building;
            Floor = command.Floor;
            Name = command.Name;
            CapacityId = command.CapacityId;
            CeilingHeight = command.CeilingHeight;
            SizeInSquareMeters = command.SizeInSquareMeters;
        }

        [JsonConstructor]
        protected Room()
        {
        }

        public void Update(ModifyRoom.Command command)
        {
            Building = command.Building;
            Floor = command.Floor;
            Name = command.Name;
            CapacityId = command.CapacityId;
            CeilingHeight = command.CeilingHeight;
            SizeInSquareMeters = command.SizeInSquareMeters;
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public string Building { get; private set; }
        public string Floor { get; private set; }
        public string Name { get; private set; }
        public int? SizeInSquareMeters { get; set; }

        public CeilingHeight? CeilingHeight { get; private set; }

        public Guid? CapacityId { get; private set; }
        public virtual SelectValueMapping Capacity { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<AppointmentRoom> AppointmentRooms { get; private set; }
            = new HashSet<AppointmentRoom>();

        [CascadingSoftDelete]
        public virtual ICollection<RoomSection> RoomSections { get; private set; }
            = new HashSet<RoomSection>();

        [CascadingSoftDelete]
        public virtual ICollection<RoomEquipment> RoomEquipments { get; private set; }
            = new HashSet<RoomEquipment>();
    }
}
