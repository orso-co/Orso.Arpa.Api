using System;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Addresses;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Venues
{
    public static class Create
    {
        public class Command : BaseAddressCreateCommand, ICreateCommand<Venue>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid? AddressId { get; set; }
        }
    }
}
