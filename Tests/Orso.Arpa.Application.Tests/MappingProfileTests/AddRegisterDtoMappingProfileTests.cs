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
    public class AddRegisterDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AddRegisterDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new AddRegisterDto
            {
                Id = Guid.NewGuid(),
                RegisterId = Guid.NewGuid()
            };

            // Act
            AddRegister.Command command = _mapper.Map<AddRegister.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
