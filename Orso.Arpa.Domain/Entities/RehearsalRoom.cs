using System;

namespace Orso.Arpa.Domain.Entities
{
    public class RehearsalRoom : BaseEntity
    {
        internal RehearsalRoom(Guid? id) : base(id)
        {
        }

        private RehearsalRoom()
        {
        }

        public Guid RoomId { get; private set; }
        public virtual Room Room { get; private set; }
    }
}
