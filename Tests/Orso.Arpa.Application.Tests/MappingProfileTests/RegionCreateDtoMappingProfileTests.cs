using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Regions;
using static Orso.Arpa.Application.Logic.Regions.Create;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var regionDto = new Dto { Name = "Name" };
            var expectedCommand = new Create.Command { Name = regionDto.Name };

            // Act
            Create.Command command = _mapper.Map<Create.Command>(regionDto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
