using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AddRoomDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentAddRoomDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentAddRoomDto
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid()
            };

            // Act
            AddRoomToAppointment.Command command = _mapper.Map<AddRoomToAppointment.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
