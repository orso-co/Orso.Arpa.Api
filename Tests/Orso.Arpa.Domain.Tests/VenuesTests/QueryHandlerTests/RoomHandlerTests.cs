using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.VenuesTests.QueryHandlerTests
{
    [TestFixture]
    public class RoomHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Venues.Rooms.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Venues.Rooms.Handler(_repository);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            Venue venue = VenueSeedData.WeiherhofSchule;
            _repository.GetByIdAsync<Venue>(Arg.Any<Guid>())
                .Returns(venue);

            // Act
            IImmutableList<Room> result = await _handler.Handle(
                new Venues.Rooms.Query(Guid.NewGuid()),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(venue.Rooms);
        }
    }
}
