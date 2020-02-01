using System;
using System.Collections.Generic;
using Orso.Arpa.Application.Logic.Rooms;
using Orso.Arpa.Application.Logic.Venues;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class VenueDtoData
    {
        public static IList<VenueDto> Venues
        {
            get
            {
                return new List<VenueDto>
                {
                    WeiherhofSchule
                };
            }
        }

        public static VenueDto WeiherhofSchule
        {
            get
            {
                var dto = new VenueDto
                {
                    Id = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    AddressId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    Name = "Weiherhof Schule",
                    Description = "Proberäume",
                    CreatedBy = "anonymous",
                    Address = new Application.Logic.Addresses.AddressDto
                    {
                        Id = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                        Address1 = "Schlüsselstraße 5",
                        Zip = "79104",
                        City = "Herdern",
                        Country = "Deutschland",
                        UrbanDistrict = "Herdern",
                        State = "Baden-Württemberg",
                        RegionId = RegionSeedData.Freiburg.Id,
                    }
                };
                dto.Rooms.Add(new RoomDto
                {
                    Id = Guid.Parse("4f5767a8-0c2d-4bf0-8623-47f040be857b"),
                    Building = "Anbau",
                    Floor = "EG",
                    Name = "Aula",
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedBy = "anonymous"
                });
                return dto;
            }
        }
    }
}
