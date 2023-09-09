using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentRemoveSectionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentRemoveSectionDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentRemoveSectionDto
            {
                Id = Guid.NewGuid(),
                SectionId = Guid.NewGuid()
            };

            // Act
            RemoveSection.Command command = _mapper.Map<RemoveSection.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
