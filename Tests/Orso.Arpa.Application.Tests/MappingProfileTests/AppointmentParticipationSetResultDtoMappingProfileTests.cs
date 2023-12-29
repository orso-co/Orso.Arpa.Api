using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AppointmentParticipationSetResultDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppointmentParticipationSetResultDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentParticipationSetResultDto
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                Body = new AppointmentParticipationSetResultBodyDto
                {
                    Result = AppointmentParticipationResult.Absent
                }
            };
            var expectedCommand = new SetAppointmentParticipationResult.Command
            {
                Id = dto.Id,
                PersonId = dto.PersonId,
                Result = AppointmentParticipationResult.Absent
            };

            // Act
            SetAppointmentParticipationResult.Command command = _mapper.Map<SetAppointmentParticipationResult.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
