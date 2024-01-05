using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class RoomSeedData
    {
        public static IList<Room> Rooms
        {
            get
            {
                return new List<Room>
                {
                    AulaWeiherhofSchule,
                    MusikraumWeiherhofSchule
                };
            }
        }

        public static Room AulaWeiherhofSchule
        {
            get
            {
                var room = new Room
                (
                    Guid.Parse("4f5767a8-0c2d-4bf0-8623-47f040be857b"),
                    new CreateRoom.Command
                    {
                        Building = "Anbau",
                        Floor = "EG",
                        Name = "Aula",
                        VenueId = VenueSeedData.WeiherhofSchule.Id,
                        CeilingHeight = CeilingHeight.High,
                        CapacityId = SelectValueMappingSeedData.RoomCapacityMappings[3].Id
                    }
                );
                room.RoomEquipments.Add(new RoomEquipment(Guid.Parse("e508ddff-2a77-4019-bebe-6d301153edf7"), SelectValueMappingSeedData.RoomEquipmentTypeMappings[6].Id, room.Id, 100));
                room.RoomEquipments.Add(new RoomEquipment(Guid.Parse("d8300d6e-93b7-48b8-b4ba-e3cbd293e6c1"), SelectValueMappingSeedData.RoomEquipmentTypeMappings[8].Id, room.Id, 1, "Bühne mit Beleuchtung"));
                room.RoomSections.Add(new RoomSection(Guid.Parse("b68a15ac-2c98-45aa-8655-c17388771783"), SectionSeedData.Piano.Id, room.Id, 1));
                return room;
            }
        }

        public static Room MusikraumWeiherhofSchule
        {
            get
            {
                return new Room
                (
                    Guid.Parse("1516e919-4088-4d95-aeb7-ff47a0c36215"),
                    new CreateRoom.Command
                    {
                        Building = "Hauptgebäude",
                        Floor = "OG",
                        Name = "Musikraum",
                        VenueId = VenueSeedData.WeiherhofSchule.Id,
                        CeilingHeight = CeilingHeight.MediumHigh,
                        CapacityId = SelectValueMappingSeedData.RoomCapacityMappings[0].Id
                    }
                );
            }
        }
    }
}
