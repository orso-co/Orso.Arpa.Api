using System;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Addresses;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Venues
{
    public static class Create
    {
        public class Command : ICreateCommand<Venue>, IAddressCreateCommand
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }
            public string UrbanDistrict { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string CommentInner { get; set; }
            public Guid? TypeId { get; set; }
            public Guid? AddressId { get; set; }
        }
    }
}
