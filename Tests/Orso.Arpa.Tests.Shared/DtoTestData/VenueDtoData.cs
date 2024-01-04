using System;
using System.Collections.Generic;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Tests.Shared.FakeData;

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
                    AddressId = Guid.Parse("9dfd22c2-41c6-463c-a4cd-334215584d56"),
                    Name = "Weiherhof Schule",
                    Description = "Proberäume",
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    Address = new AddressDto
                    {
                        Id = Guid.Parse("9dfd22c2-41c6-463c-a4cd-334215584d56"),
                        Address1 = "Schlüsselstraße 5",
                        Zip = "79104",
                        City = "Freiburg",
                        Country = "Deutschland",
                        UrbanDistrict = "Herdern",
                        State = "Baden-Württemberg",
                        CreatedBy = "anonymous",
                        CreatedAt = FakeDateTime.UtcNow
                    }
                };
                dto.Rooms.Add(RoomDtoData.AulaWeiherhofSchule);
                dto.Rooms.Add(RoomDtoData.MusikraumWeiherhofSchule);
                return dto;
            }
        }
    }
}
