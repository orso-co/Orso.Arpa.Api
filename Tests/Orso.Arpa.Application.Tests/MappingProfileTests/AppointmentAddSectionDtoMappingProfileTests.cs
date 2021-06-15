using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentAddSectionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentAddSectionDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentAddSectionDto
            {
                Id = Guid.NewGuid(),
                SectionId = Guid.NewGuid()
            };

            // Act
            AddSection.Command command = _mapper.Map<AddSection.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
