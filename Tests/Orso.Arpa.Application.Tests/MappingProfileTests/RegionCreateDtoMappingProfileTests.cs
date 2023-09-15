using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RegionCreateDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var regionDto = new RegionCreateDto { Name = "Name" };
            var expectedCommand = new CreateRegion.Command { Name = regionDto.Name };

            // Act
            CreateRegion.Command command = _mapper.Map<CreateRegion.Command>(regionDto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
