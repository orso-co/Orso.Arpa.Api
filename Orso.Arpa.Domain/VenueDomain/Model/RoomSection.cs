using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    /// <summary>
    /// This class can be used to specify which instruments are available in a room.
    /// </summary>
    public class RoomSection : BaseEntity
    {
        public RoomSection(Guid? id, Section section, Room room, int? count, string description) : base(id)
        {
            Section = section;
            Room = room;
            Quantity = count;
            Description = description;
        }

        public RoomSection(Guid? id, CreateRoomSection.Command command) : base(id)
        {
            SectionId = command.SectionId;
            RoomId = command.RoomId;
            Quantity = command.Quantity;
            Description = command.Description;
        }

        public RoomSection()
        {
        }

        public void Update (ModifyRoomSection.Command command)
        {
            Quantity = command.Quantity;
            Description = command.Description;
        }

        public Guid RoomId { get; private set; }
        public virtual Room Room { get; private set; }
        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public int? Quantity { get; private set; }
        public string Description { get; set; }
    }
}