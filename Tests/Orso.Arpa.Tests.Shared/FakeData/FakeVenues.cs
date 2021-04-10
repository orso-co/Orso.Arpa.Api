using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeVenues
    {
        public static Venue WeiherhofSchule
        {
            get
            {
                Venue venue = VenueSeedData.WeiherhofSchule;
                venue.Rooms.Add(FakeRooms.AulaWeiherhofSchule);
                venue.Rooms.Add(FakeRooms.MusikraumWeiherhofSchule);
                venue.SetProperty(nameof(Venue.CreatedBy), "anonymous");
                venue.SetProperty(nameof(Venue.CreatedAt), FakeDateTime.UtcNow);
                venue.Address.SetProperty(nameof(Address.CreatedBy), "anonymous");
                venue.Address.SetProperty(nameof(Address.CreatedAt), FakeDateTime.UtcNow);
                return venue;
            }
        }
    }
}
