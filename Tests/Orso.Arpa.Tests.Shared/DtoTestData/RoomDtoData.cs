using System;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RoomDtoData
    {
        public static RoomDto AulaWeiherhofSchule
        {
            get
            {
                var dto = new RoomDto
                {
                    Id = Guid.Parse("4f5767a8-0c2d-4bf0-8623-47f040be857b"),
                    Building = "Anbau",
                    Floor = "EG",
                    Name = "Aula",
                    VenueId = Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    CeilingHeight = CeilingHeight.High,
                    Capacity = SelectValueDtoData.Tutti
                };
                dto.AvailableInstruments.Add(new RoomSectionDto
                {
                    Id = Guid.Parse("b68a15ac-2c98-45aa-8655-c17388771783"),
                    Name = "Piano",
                    Count = 1,
                    Description = null
                });
                dto.AvailableEquipment.Add(new RoomEquipmentDto
                {
                    Id = Guid.Parse("e508ddff-2a77-4019-bebe-6d301153edf7"),
                    Name = "Chairs",
                    Count = 100,
                    Description = null
                });
                dto.AvailableEquipment.Add(new RoomEquipmentDto
                {
                    Id = Guid.Parse("d8300d6e-93b7-48b8-b4ba-e3cbd293e6c1"),
                    Name = "Stage",
                    Count = 1,
                    Description = "Bühne mit Beleuchtung"
                });
                return dto;
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
                    CreatedAt = FakeDateTime.UtcNow,
                    CeilingHeight = CeilingHeight.MediumHigh,
                    Capacity = SelectValueDtoData.VoiceRehearsal
                };
            }
        }
    }
}
