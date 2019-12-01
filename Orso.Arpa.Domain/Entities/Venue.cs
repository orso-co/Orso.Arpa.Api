using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class Venue : BaseEntity
    {
        internal Venue(Guid? id) : base(id)
        {
        }

        protected Venue()
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid AddressId { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual ICollection<Room> Rooms { get; private set; } = new HashSet<Room>();
    }
}
