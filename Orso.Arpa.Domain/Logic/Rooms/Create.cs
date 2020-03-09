using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Rooms
{
    public static class Create
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
