using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentRemoveRoomDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentRemoveRoomDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentRemoveRoomDto
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid()
            };

            // Act
            RemoveRoomFromAppointment.Command command = _mapper.Map<RemoveRoomFromAppointment.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
