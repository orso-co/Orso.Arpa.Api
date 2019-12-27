using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Venues;

namespace Orso.Arpa.Domain.Entities
{
    public class Venue : BaseEntity
    {
        public Venue(Guid? id, Create.Command command) : base(id)
        {
            Name = command.Name;
            Description = command.Description;
            Address = new Address(id, command);
        }

        [JsonConstructor]
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
