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
                venue.SetProperty(nameof(Venue.CreatedBy), "anonymous");
                return venue;
            }
        }
    }
}
