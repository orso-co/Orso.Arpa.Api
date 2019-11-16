using System;

namespace Orso.Arpa.Domain.Entities
{
    public class ConcertRoom : BaseEntity
    {
        internal ConcertRoom(Guid? id) : base(id)
        {
        }

        private ConcertRoom()
        {
        }

        public Guid RoomId { get; private set; }
        public virtual Room Room { get; private set; }
    }
}
