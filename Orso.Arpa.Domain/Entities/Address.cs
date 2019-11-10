using System;

namespace Orso.Arpa.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address(Guid id) : base(id)
        {
        }

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Zip { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public Guid? RegionId { get; private set; }
        public virtual Region Region { get; private set; }
        public Guid? PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
