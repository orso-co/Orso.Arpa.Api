using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Rooms
{
    public static class Create
    {
        public class Command : IRequest<Room>
        {
            public string Name { get; set; }
            public Guid VenueId { get; set; }
            public string Building { get; set; }
            public string Floor { get; set; }
        }
    }
}
