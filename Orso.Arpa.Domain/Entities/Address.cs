using System;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Logic.Venues;

namespace Orso.Arpa.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address(Guid? id, Create.Command command) : base(id)
        {
            Address1 = command.Address1;
            Address2 = command.Address2;
            Zip = command.Zip;
            City = command.City;
            UrbanDistrict = command.UrbanDistrict;
            Country = command.Country;
            State = command.State;
            RegionId = command.RegionId;
        }

        internal Address(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Address()
        {
        }

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Zip { get; private set; }
        public string City { get; private set; }
        public string UrbanDistrict { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public Guid? RegionId { get; private set; }
        public virtual Region Region { get; private set; }

        public virtual Venue Venue { get; private set; }
    }
}
