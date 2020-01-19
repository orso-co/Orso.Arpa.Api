using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.AppointmentParticipations;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetParticipationResultDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetParticipationResultDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetParticipationResultDto
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                ResultId = Guid.NewGuid()
            };

            // Act
            SetResult.Command command = _mapper.Map<SetResult.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
