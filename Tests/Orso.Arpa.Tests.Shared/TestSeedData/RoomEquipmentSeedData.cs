using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class RoomEquipmentSeedData
    {
        public static IList<RoomEquipment> RoomEquipments
        {
            get
            {
                return
                [
                    AulaWeiherhofSchuleChairs,
                    AulaWeiherhofschuleStage
                ];
            }
        }

        public static RoomEquipment AulaWeiherhofSchuleChairs
        {
            get
            {
                return new RoomEquipment(
                    Guid.Parse("e508ddff-2a77-4019-bebe-6d301153edf7"),
                    new CreateRoomEquipment.Command
                    {
                        RoomId = RoomSeedData.AulaWeiherhofSchule.Id,
                        EquipmentId = SelectValueMappingSeedData.RoomEquipmentTypeMappings[6].Id,
                        Quantity = 100
                    });
            }
        }

        public static RoomEquipment AulaWeiherhofschuleStage
        {
            get
            {
                return new RoomEquipment(
                    Guid.Parse("d8300d6e-93b7-48b8-b4ba-e3cbd293e6c1"),
                    new CreateRoomEquipment.Command
                    {
                        RoomId = RoomSeedData.AulaWeiherhofSchule.Id,
                        EquipmentId = SelectValueMappingSeedData.RoomEquipmentTypeMappings[8].Id,
                        Quantity = 1,
                        Description = "Bühne mit Beleuchtung"
                    });
            }
        }
    }
}
