using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Appointments;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class AddSectionDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AddSectionDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AddSectionDto
            {
                Id = Guid.NewGuid(),
                SectionId = Guid.NewGuid()
            };

            // Act
            AddSection.Command command = _mapper.Map<AddSection.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
