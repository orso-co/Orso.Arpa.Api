using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Domain.Logic.MusicianProfileDeactivations;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class MusicianProfileDeactivationCreateDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MusicianProfileDeactivationCreateDtoMappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new MusicianProfileDeactivationCreateDto
            {
                Id = Guid.NewGuid(),
                Body = new MusicianProfileDeactivationCreateBodyDto
                {
                    DeactivationStart = DateTime.UtcNow,
                    Purpose = "Purpose"
                }
            };

            // Act
            Create.Command command = _mapper.Map<Create.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto.Body);
            command.MusicianProfileId.Should().Be(dto.Id);
        }
    }
}
