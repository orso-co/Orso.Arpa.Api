using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class PersonAddress : BaseEntity
    {
        public PersonAddress(Guid? id, CreateAddress.Command command) : this(id, (AddressCommand)command) {
            PersonId = command.PersonId;
        }

        private PersonAddress(Guid? id, AddressCommand command) : base(id)
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

        internal PersonAddress(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected PersonAddress()
        {
        }

        public void Update(ModifyAddress.Command command)
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

        public void Update(ModifyVenue.Command command)
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
