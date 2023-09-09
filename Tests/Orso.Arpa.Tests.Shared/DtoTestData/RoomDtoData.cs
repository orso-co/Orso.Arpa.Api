using System;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RoomDtoData
    {
        public static RoomDto AulaWeiherhofSchule
        {
            get
            {
                return new RoomDto
                {
                    Id = Guid.Parse("4f5767a8-0c2d-4bf0-8623-47f040be857b"),
                    Building = "Anbau",
                    Floor = "EG",
                    Name = "Aula",
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static RoomDto MusikraumWeiherhofSchule
        {
            get
            {
                return new RoomDto
                {
                    Id = Guid.Parse("1516e919-4088-4d95-aeb7-ff47a0c36215"),
                    Building = "Hauptgebäude",
                    CreatedBy = "anonymous",
                    Floor = "OG",
                    Name = "Musikraum",
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }
    }
}
