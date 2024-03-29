using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentSetVenueDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentSetVenueDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentSetVenueDto
            {
                Id = Guid.NewGuid(),
                VenueId = Guid.NewGuid()
            };

            // Act
            SetVenue.Command command = _mapper.Map<SetVenue.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
