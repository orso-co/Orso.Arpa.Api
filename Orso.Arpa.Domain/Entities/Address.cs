using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Addresses;

namespace Orso.Arpa.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Address(Guid? id, IAddressCreateCommand command) : base(id)
        {
            Address1 = command.Address1;
            Address2 = command.Address2;
            Zip = command.Zip;
            City = command.City;
            UrbanDistrict = command.UrbanDistrict;
            Country = command.Country;
            State = command.State;
            CommentInner = command.CommentInner;
            TypeId = command.TypeId;
        }

        internal Address(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Address()
        {
        }

        public void Update(Modify.Command command)
        {
            Address1 = command.Address1;
            Address2 = command.Address2;
            Zip = command.Zip;
            City = command.City;
            UrbanDistrict = command.UrbanDistrict;
            Country = command.Country;
            State = command.State;
            CommentInner = command.CommentInner;
            TypeId = command.TypeId;
        }

        public void Update(Logic.Venues.Modify.Command command)
        {
            Address1 = command.Address1;
            Address2 = command.Address2;
            Zip = command.Zip;
            City = command.City;
            UrbanDistrict = command.UrbanDistrict;
            Country = command.Country;
            State = command.State;
            CommentInner = command.CommentInner;
        }

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Zip { get; private set; }
        public string City { get; private set; }
        public string UrbanDistrict { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public string CommentInner { get; private set; }
        public Guid? TypeId { get; private set; }
        public virtual SelectValueMapping Type { get; private set; }
        public Guid? PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
