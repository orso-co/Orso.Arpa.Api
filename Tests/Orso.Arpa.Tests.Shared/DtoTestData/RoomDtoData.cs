using System;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
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
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    CeilingHeight = CeilingHeight.High,
                    Capacity = SelectValueDtoData.Tutti
                };
                dto.AvailableInstruments.Add(RoomSectionDtoData.AulaWeiherhofSchulePiano);
                dto.AvailableEquipment.Add(RoomEquipmentDtoData.AulaWeiherhofSchuleChairs);
                dto.AvailableEquipment.Add(RoomEquipmentDtoData.AulaWeiherhofSchuleStage);
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
                    CreatedAt = FakeDateTime.UtcNow,
                    CeilingHeight = CeilingHeight.MediumHigh,
                    Capacity = SelectValueDtoData.VoiceRehearsal
                };
            }
        }
    }
}
