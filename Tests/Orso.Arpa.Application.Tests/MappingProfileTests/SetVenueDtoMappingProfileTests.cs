using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetVenueDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetVenueDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetVenueDto
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
