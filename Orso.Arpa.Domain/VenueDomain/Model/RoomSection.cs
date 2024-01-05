using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.VenueDomain.Model
{
    /*
    * This class can be used to specify which instruments are available in a room.
    */
    public class RoomSection : BaseEntity
    {
        public RoomSection(Guid? id, Section section, Room room, int count, string description) : base(id)
        {
            Section = section;
            Room = room;
            Count = count;
            Description = description;
        }

        public RoomSection(Guid? id, Guid sectionId, Guid roomId, int count, string description = null) : base(id)
        {
            SectionId = sectionId;
            RoomId = roomId;
            Count = count;
            Description = description;
        }

        public RoomSection()
        {
        }

        public Guid RoomId { get; private set; }
        public virtual Room Room { get; private set; }
        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public int Count { get; private set; }
        public string Description { get; set; }
    }
}