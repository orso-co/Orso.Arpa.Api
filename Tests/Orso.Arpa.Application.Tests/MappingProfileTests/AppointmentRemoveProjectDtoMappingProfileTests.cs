using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentRemoveProjectDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentRemoveProjectDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentRemoveProjectDto
            {
                Id = Guid.NewGuid(),
                ProjectId = Guid.NewGuid()
            };

            // Act
            RemoveProjectFromAppointment.Command command = _mapper.Map<RemoveProjectFromAppointment.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
