using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetProjectParticipationDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetProjectParticipationDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new SetProjectParticipationDto
            {
                Id = Guid.NewGuid(),
                Body = new SetProjectParticipationBodyDto
                {
                    CommentByStaffInner = "CommentByStaffInner",
                    CommentTeam = "CommentTeam",
                    InvitationStatusId = Guid.NewGuid(),
                    MusicianProfileId = Guid.NewGuid(),
                    ParticipationStatusInnerId = Guid.NewGuid(),
                    ParticipationStatusInternalId = Guid.NewGuid()
                }
            };
            var expectedCommand = new SetProjectParticipation.Command
            {
                ProjectId = dto.Id,
                CommentByStaffInner = dto.Body.CommentByStaffInner,
                CommentTeam = dto.Body.CommentTeam,
                InvitationStatusId = dto.Body.InvitationStatusId,
                MusicianProfileId = dto.Body.MusicianProfileId,
                ParticipationStatusInnerId = dto.Body.ParticipationStatusInnerId,
                ParticipationStatusInternalId = dto.Body.ParticipationStatusInternalId
            };

            // Act
            SetProjectParticipation.Command command = _mapper.Map<SetProjectParticipation.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
