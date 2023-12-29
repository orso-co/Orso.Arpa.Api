using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RegionModifyDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RegionModifyDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var regionDto = new RegionModifyDto { Id = Guid.NewGuid(), Body = new RegionModifyBodyDto { Name = "Name" } };
            var expectedCommand = new ModifyRegion.Command { Id = regionDto.Id, Name = regionDto.Body.Name };

            // Act
            ModifyRegion.Command command = _mapper.Map<ModifyRegion.Command>(regionDto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
