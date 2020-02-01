using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Regions;
using static Orso.Arpa.Application.Logic.Regions.Modify;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionModifyDtoMappingProfileTests
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
            var regionDto = new Dto { Id = Guid.NewGuid(), Name = "Name" };
            var expectedCommand = new Modify.Command { Id = regionDto.Id, Name = regionDto.Name };

            // Act
            Modify.Command command = _mapper.Map<Modify.Command>(regionDto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
