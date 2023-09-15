using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Application.VenueApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
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
                cfg.AddProfile<AddressDtoMappingProfile>();
                cfg.AddProfile<RoomDtoMappingProfile>();
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
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
