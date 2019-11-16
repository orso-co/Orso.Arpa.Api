using System;

namespace Orso.Arpa.Domain.Entities
{
    public class Room : BaseEntity
    {
        internal Room(Guid? id) : base(id)
        {
        }

        private Room()
        {
        }

        public virtual RehearsalRoom RehearsalRoom { get; private set; }
        public virtual ConcertRoom ConcertRoom { get; private set; }
        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public string Building { get; private set; }
        public string Floor { get; private set; }
        public string Name { get; private set; }
    }
}
