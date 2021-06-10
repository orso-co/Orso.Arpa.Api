using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Projects;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.ProjectsTests.MappingProfileTests
{
    [TestFixture]
    public class SetProjectParticipationCommandMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetProjectParticipation.MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange

            ProjectParticipation projectParticipation = ProjectParticipationSeedData.StaffParticipation1;

            var command = new SetProjectParticipation.Command
            {
                CommentByStaffInner = "CommantByStaffInner",
                CommentTeam = "CommentTeam",
                InvitationStatusId = Guid.NewGuid(),
                MusicianProfileId = Guid.NewGuid(),
                ParticipationStatusInnerId = Guid.NewGuid(),
                ParticipationStatusInternalId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid()
            };
            ProjectParticipation expectedParticipation = ProjectParticipationSeedData.StaffParticipation1;
            expectedParticipation.SetProperty(nameof(projectParticipation.CommentByStaffInner), command.CommentByStaffInner);
            expectedParticipation.SetProperty(nameof(projectParticipation.CommentTeam), command.CommentTeam);
            expectedParticipation.SetProperty(nameof(projectParticipation.ParticipationStatusInnerId), command.ParticipationStatusInnerId);
            expectedParticipation.SetProperty(nameof(projectParticipation.ParticipationStatusInternalId), command.ParticipationStatusInternalId);
            expectedParticipation.SetProperty(nameof(projectParticipation.InvitationStatusId), command.InvitationStatusId);

            // Act
            ProjectParticipation mappedParticipation = _mapper.Map(command, projectParticipation);

            // Assert
            mappedParticipation.Should().BeEquivalentTo(expectedParticipation);
        }
    }
}
