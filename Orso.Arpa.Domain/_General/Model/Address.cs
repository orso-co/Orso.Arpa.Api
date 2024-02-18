using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain._General.Model
{
    [ComplexType]
    public class Address {

        public Address(AddressCommand command)
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

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string UrbanDistrict { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string CommentInner { get; set; }
        public Guid? TypeId { get; set; }
        public virtual SelectValueMapping Type { get; private set; }
    }
}