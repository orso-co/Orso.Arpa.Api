using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Queries;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.VenuesTests.QueryHandlerTests
{
    [TestFixture]
    public class RoomHandlerTests
    {
        private IArpaContext _arpaContext;
        private ListRooms.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new ListRooms.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            Venue venue = VenueSeedData.WeiherhofSchule;
            Microsoft.EntityFrameworkCore.DbSet<Venue> mockVenues = MockDbSets.Venues;
            mockVenues.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(venue);
            _arpaContext.Venues.Returns(mockVenues);

            // Act
            IImmutableList<Room> result = await _handler.Handle(
                new ListRooms.Query(venue.Id),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(venue.Rooms);
        }
    }
}
