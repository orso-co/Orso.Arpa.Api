using System;

namespace Orso.Arpa.Domain.Entities
{
    public class ConcertRoom : BaseEntity
    {
        public ConcertRoom(Guid id) : base(id)
        {
        }

        public Guid RoomId { get; private set; }
        public virtual Room Room { get; private set; }
    }
}
