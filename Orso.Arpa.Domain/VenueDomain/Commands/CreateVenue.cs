using System;
using Orso.Arpa.Domain.AddressDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class CreateVenue
    {
        public class Command : AddressCommand, ICreateCommand<Venue>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid? AddressId { get; set; }
        }
    }
}
