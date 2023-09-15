using System;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class CreateRoom
    {
        public class Command : ICreateCommand<Room>
        {
            public string Name { get; set; }
            public Guid VenueId { get; set; }
            public string Building { get; set; }
            public string Floor { get; set; }
        }
    }
}
