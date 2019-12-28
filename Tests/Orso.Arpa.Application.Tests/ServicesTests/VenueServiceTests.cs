using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class VenueServiceTests
    {
        private IMediator _subMediator;
        private IMapper _subMapper;

        [SetUp]
        public void SetUp()
        {
            _subMediator = Substitute.For<IMediator>();
            _subMapper = Substitute.For<IMapper>();
        }

        private VenueService CreateService()
        {
            return new VenueService(
                _subMediator,
                _subMapper);
        }

        [Test]
        public async Task GetAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            VenueService service = CreateService();
            IList<VenueDto> expectedDtos = VenueDtoData.Venues;
            _subMapper.Map<IEnumerable<VenueDto>>(Arg.Any<IEnumerable<Venue>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<VenueDto> result = await service.GetAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }

        [Test]
        public async Task GetRoomsAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            VenueService service = CreateService();
            Guid id = Guid.Empty;
            IList<RoomDto> expectedDtos = VenueDtoData.WeiherhofSchule.Rooms;
            _subMapper.Map<IEnumerable<RoomDto>>(Arg.Any<IEnumerable<Room>>())
                .Returns(expectedDtos);

            // Act
            IEnumerable<RoomDto> result = await service.GetRoomsAsync(
                id);

            // Assert
            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
