using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.AppointmentParticipations;
using static Orso.Arpa.Application.Logic.AppointmentParticipations.SetResult;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetParticipationResultDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new Dto
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
