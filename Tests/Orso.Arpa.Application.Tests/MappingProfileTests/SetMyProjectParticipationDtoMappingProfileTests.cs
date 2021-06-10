using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetMyProjectParticipationDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetMyProjectParticipationDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetMyProjectParticipationDto
            {
                Id = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                Body = new SetMyProjectParticipationBodyDto
                {
                    Comment = "Comment",
                    StatusId = Guid.NewGuid()
                }
            };
            var expectedCommand = new SetProjectParticipation.Command
            {
                MusicianProfileId = dto.Id,
                ProjectId = dto.ProjectId,
                Comment = dto.Body.Comment,
                PersonId = Guid.Empty,
                StatusId = dto.Body.StatusId
            };

            // Act
            SetProjectParticipation.Command command = _mapper.Map<SetProjectParticipation.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
