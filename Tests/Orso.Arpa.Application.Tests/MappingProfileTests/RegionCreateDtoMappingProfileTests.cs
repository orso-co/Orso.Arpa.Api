using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionCreateDtoMappingProfileTests : DtoMappingProfileTestBase
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
            var expectedCommand = new Create.Command { Name = regionDto.Name };

            // Act
            Create.Command command = _mapper.Map<Create.Command>(regionDto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
