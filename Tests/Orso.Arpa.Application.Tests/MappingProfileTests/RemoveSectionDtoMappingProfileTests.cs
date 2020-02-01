using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Appointments;
using static Orso.Arpa.Application.Logic.Appointments.RemoveSection;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RemoveSectionDtoMappingProfileTests
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
                SectionId = Guid.NewGuid()
            };

            // Act
            RemoveSection.Command command = _mapper.Map<RemoveSection.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
