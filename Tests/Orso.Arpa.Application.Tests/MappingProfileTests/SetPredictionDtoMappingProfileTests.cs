using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Me;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetPredictionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetPrediction.MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetPrediction.Dto
            {
                Id = Guid.NewGuid(),
                PredictionId = Guid.NewGuid()
            };

            // Act
            AppointmentParticipations.SetPrediction.Command command = _mapper.Map<AppointmentParticipations.SetPrediction.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
