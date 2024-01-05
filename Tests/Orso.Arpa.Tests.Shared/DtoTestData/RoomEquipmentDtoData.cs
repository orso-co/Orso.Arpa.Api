using System;
using Orso.Arpa.Application.RoomApplication.Model;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class RoomEquipmentDtoData
    {
        public static RoomEquipmentDto AulaWeiherhofSchuleChairs
        {
            get
            {
                return new RoomEquipmentDto
                {
                    Id = Guid.Parse("e508ddff-2a77-4019-bebe-6d301153edf7"),
                    Name = "Chairs",
                    Quantity = 100,
                    Description = null
                };
            }
        }

        public static RoomEquipmentDto AulaWeiherhofSchuleStage
        {
            get
            {
                return new RoomEquipmentDto
                {
                    Id = Guid.Parse("d8300d6e-93b7-48b8-b4ba-e3cbd293e6c1"),
                    Name = "Stage",
                    Quantity = 1,
                    Description = "Bühne mit Beleuchtung"
                };
            }
        }
    }
}
