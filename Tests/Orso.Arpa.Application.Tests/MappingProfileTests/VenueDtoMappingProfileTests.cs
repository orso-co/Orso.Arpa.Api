using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.VenueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class VenueDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VenueDtoMappingProfile>();
                cfg.AddProfile<AddressApplication.AddressDtoMappingProfile>();
                cfg.AddProfile<RoomApplication.RoomDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Venue venue = FakeVenues.WeiherhofSchule;
            VenueDto expectedDto = VenueDtoData.WeiherhofSchule;

            // Act
            VenueDto dto = _mapper.Map<VenueDto>(venue);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dto => dto.AddressId)
                .Excluding(dto => dto.Rooms));
            dto.AddressId.Should().Be(expectedDto.Id);
            dto.Rooms.Should().BeEquivalentTo(expectedDto.Rooms);
        }
    }
}
