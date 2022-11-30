using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;

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

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AppointmentParticipationSetResultDto
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                Result = AppointmentParticipationResult.Absent
            };

            // Act
            SetResult.Command command = _mapper.Map<SetResult.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(dto);
        }
    }
}
