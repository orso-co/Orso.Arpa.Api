using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeRooms
    {
        public static Room AulaWeiherhofSchule
        {
            get
            {
                Room room = RoomSeedData.AulaWeiherhofSchule;
                room.SetProperty(nameof(Room.CreatedBy), "anonymous");
                room.SetProperty(nameof(Room.CreatedAt), FakeDateTime.UtcNow);
                room.SetProperty(nameof(Room.Capacity), FakeSelectValueMappings.Tutti);
                room.RoomEquipments.Add(AulaWeiherhofschuleChairs);
                room.RoomEquipments.Add(AulaWeiherhofschuleStage);
                room.RoomSections.Add(AulaWeiherhofSchulePiano);
                return room;
            }
        }

        public static RoomSection AulaWeiherhofSchulePiano
        {
            get
            {
                RoomSection roomSection = RoomSectionSeedData.AulaWeiherhofSchulePiano;
                roomSection.SetProperty(nameof(RoomSection.CreatedBy), "anonymous");
                roomSection.SetProperty(nameof(RoomSection.CreatedAt), FakeDateTime.UtcNow);
                roomSection.SetProperty(nameof(RoomSection.Section), SectionSeedData.Piano);
                return roomSection;
            }
        }

        public static RoomEquipment AulaWeiherhofschuleChairs {
            get
            {
                RoomEquipment roomEquipment = RoomEquipmentSeedData.AulaWeiherhofSchuleChairs;
                roomEquipment.SetProperty(nameof(RoomEquipment.CreatedBy), "anonymous");
                roomEquipment.SetProperty(nameof(RoomEquipment.CreatedAt), FakeDateTime.UtcNow);
                roomEquipment.SetProperty(nameof(RoomEquipment.Equipment), FakeSelectValueMappings.Chairs);
                return roomEquipment;
            }
        }

        public static RoomEquipment AulaWeiherhofschuleStage {
            get
            {
                RoomEquipment roomEquipment = RoomEquipmentSeedData.AulaWeiherhofschuleStage;
                roomEquipment.SetProperty(nameof(RoomEquipment.CreatedBy), "anonymous");
                roomEquipment.SetProperty(nameof(RoomEquipment.CreatedAt), FakeDateTime.UtcNow);
                roomEquipment.SetProperty(nameof(RoomEquipment.Equipment), FakeSelectValueMappings.Stage);
                return roomEquipment;
            }
        }

        public static Room MusikraumWeiherhofSchule
        {
            get
            {
                Room room = RoomSeedData.MusikraumWeiherhofSchule;
                room.SetProperty(nameof(Room.CreatedBy), "anonymous");
                room.SetProperty(nameof(Room.CreatedAt), FakeDateTime.UtcNow);
                room.SetProperty(nameof(Room.Capacity), FakeSelectValueMappings.VoiceRehearsal);
                return room;
            }
        }
    }
}
