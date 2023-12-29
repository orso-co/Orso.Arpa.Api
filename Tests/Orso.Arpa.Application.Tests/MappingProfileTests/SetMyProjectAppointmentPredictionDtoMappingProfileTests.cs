using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetMyProjectAppointmentPredictionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetMyProjectAppointmentPredictionDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetMyAppointmentParticipationPredictionDto
            {
                Id = Guid.NewGuid(),
                Body = new SetMyAppointmentParticipationPredictionBodyDto
                {
                    CommentByPerformerInner = "CommentByPerformerInner",
                    Prediction = AppointmentParticipationPrediction.Partly
                }
            };
            var expectedCommand = new SetAppointmentParticipationPrediction.Command
            {
                CommentByPerformerInner = dto.Body.CommentByPerformerInner,
                Id = dto.Id,
                PersonId = Guid.Empty,
                Prediction = AppointmentParticipationPrediction.Partly
            };

            // Act
            SetAppointmentParticipationPrediction.Command command = _mapper.Map<SetAppointmentParticipationPrediction.Command>(dto);

            // Assert
            _ = command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
