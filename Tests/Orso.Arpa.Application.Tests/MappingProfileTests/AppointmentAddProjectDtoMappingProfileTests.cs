using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentAddProjectDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentAddProjectDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentAddProjectDto
            {
                Id = Guid.NewGuid(),
                ProjectId = Guid.NewGuid()
            };

            // Act
            AddProject.Command command = _mapper.Map<AddProject.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
