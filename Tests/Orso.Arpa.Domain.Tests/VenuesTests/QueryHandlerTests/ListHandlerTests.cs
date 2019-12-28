using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.VenuesTests.QueryHandlerTests
{
    [TestFixture]
    public class ListHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Venues.List.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Venues.List.Handler(_repository);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            var expectedVenues = VenueSeedData.Venues.ToImmutableList();
            IQueryable<Venue> venuesToReturn = VenueSeedData.Venues.AsQueryable().BuildMock();
            _repository.GetAll<Venue>()
                .Returns(venuesToReturn);

            // Act
            IImmutableList<Venue> result = await _handler.Handle(
                new Venues.List.Query(),
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedVenues);
        }
    }
}
