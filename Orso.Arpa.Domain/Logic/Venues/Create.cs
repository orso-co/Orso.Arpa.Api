using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Logic.Venues
{
    public static class Create
    {
        public class Command : IRequest<Venue>
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
            public Guid? RegionId { get; set; }
        }
    }
}
