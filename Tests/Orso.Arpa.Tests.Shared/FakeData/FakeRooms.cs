using Orso.Arpa.Domain.Entities;
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
                return room;
            }
        }

        public static Room MusikraumWeiherhofSchule
        {
            get
            {
                Room room = RoomSeedData.MusikraumWeiherhofSchule;
                room.SetProperty(nameof(Room.CreatedBy), "anonymous");
                room.SetProperty(nameof(Room.CreatedAt), FakeDateTime.UtcNow);
                return room;
            }
        }
    }
}
