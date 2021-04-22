using System;
using System.Collections.Generic;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.RoomApplication;
using Orso.Arpa.Application.VenueApplication;
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
                    CreatedAt = new DateTime(2021, 1, 1),
                    Address = new AddressDto
                    {
                        Id = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                        Address1 = "Schlüsselstraße 5",
                        Zip = "79104",
                        City = "Herdern",
                        Country = "Deutschland",
                        UrbanDistrict = "Herdern",
                        State = "Baden-Württemberg",
                        RegionId = RegionSeedData.Freiburg.Id,
                        CreatedBy = "anonymous",
                        CreatedAt = new DateTime(2021, 1, 1)
                    }
                };
                dto.Rooms.Add(new RoomDto
                {
                    Id = Guid.Parse("4f5767a8-0c2d-4bf0-8623-47f040be857b"),
                    Building = "Anbau",
                    Floor = "EG",
                    Name = "Aula",
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedBy = "anonymous",
                    CreatedAt = new DateTime(2021,1,1)
                });
                dto.Rooms.Add(new RoomDto
                {
                    Id = Guid.Parse("1516e919-4088-4d95-aeb7-ff47a0c36215"),
                    Building = "Hauptgebäude",
                    CreatedBy = "anonymous",
                    Floor = "OG",
                    Name = "Musikraum",
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = new DateTime(2021, 1, 1)
                });
                return dto;
            }
        }
    }
}
