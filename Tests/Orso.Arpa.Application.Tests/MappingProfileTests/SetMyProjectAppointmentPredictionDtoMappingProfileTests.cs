using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;

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

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetMyAppointmentParticipationPredictionDto
            {
                Id = Guid.NewGuid(),
                PredictionId = Guid.NewGuid(),
                Body = new SetMyAppointmentParticipationPredictionBodyDto
                {
                    CommentByPerformerInner = "CommentByPerformerInner"
                }
            };
            var expectedCommand = new AppointmentParticipations.SetPrediction.Command
            {
                CommentByPerformerInner = dto.Body.CommentByPerformerInner,
                Id = dto.Id,
                PersonId = Guid.Empty,
                PredictionId = dto.PredictionId,
            };

            // Act
            AppointmentParticipations.SetPrediction.Command command = _mapper.Map<AppointmentParticipations.SetPrediction.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
