using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RemoveProjectDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RemoveProjectDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new RemoveProjectDto
            {
                Id = Guid.NewGuid(),
                ProjectId = Guid.NewGuid()
            };

            // Act
            RemoveProject.Command command = _mapper.Map<RemoveProject.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
