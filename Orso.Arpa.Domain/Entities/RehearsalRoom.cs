using System;

namespace Orso.Arpa.Domain.Entities
{
    public class RehearsalRoom : BaseEntity
    {
        public RehearsalRoom(Guid id) : base(id)
        {
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
    }
}
