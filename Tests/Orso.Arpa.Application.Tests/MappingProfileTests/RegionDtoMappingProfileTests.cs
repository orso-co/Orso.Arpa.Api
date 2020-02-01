using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Regions;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Region region = RegionSeedData.Berlin;
            RegionDto expectedDto = RegionDtoData.Berlin;

            // Act
            RegionDto dto = _mapper.Map<RegionDto>(region);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
