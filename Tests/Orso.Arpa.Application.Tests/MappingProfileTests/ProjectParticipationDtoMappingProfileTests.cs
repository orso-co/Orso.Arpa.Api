using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectParticipationDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<RoleBasedSetNullAction<ProjectParticipation, ProjectParticipationDto>>();
            services.AddSingleton(_tokenAccessor);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProjectParticipationDtoMappingProfile>();
                cfg.AddProfile<BaseEntityDtoMappingProfile>();
                cfg.AddProfile<ReducedMusicianProfileDtoMappingProfile>();
                cfg.AddProfile<ReducedProjectDtoMappingProfile>();
                cfg.AddProfile<ReducedPersonDtoMappingProfile>();
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        private IMapper _mapper;
        private readonly ITokenAccessor _tokenAccessor = Substitute.For<ITokenAccessor>();

        [Test]
        public void Should_Map_For_Staff()
        {
            // Arrange
            ProjectParticipation projectParticipation = FakeProjectParticipations.PerformerSchneeköniginParticipation;
            _tokenAccessor.UserRoles.Returns(new List<string> { RoleNames.Staff });
            var expectedDto = new ProjectParticipationDto
            {
                Id = projectParticipation.Id,
                CommentByPerformerInner = projectParticipation.CommentByPerformerInner,
                CommentByStaffInner = projectParticipation.CommentByStaffInner,
                CommentTeam = projectParticipation.CommentTeam,
                InvitationStatus = "Invited",
                InvitationStatusId = projectParticipation.InvitationStatusId,
                ParticipationStatusInternal = "Candidate",
                ParticipationStatusInternalId = projectParticipation.ParticipationStatusInternalId,
                ParticipationStatusInner = "Acceptance",
                ParticipationStatusInnerId = projectParticipation.ParticipationStatusInnerId,
                MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                Project = ReducedProjectDtoData.Schneekönigin,
                Person = ReducedPersonDtoData.Performer
            };

            // Act
            ProjectParticipationDto dto = _mapper.Map<ProjectParticipationDto>(projectParticipation);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void Should_Map_For_Performer()
        {
            // Arrange
            ProjectParticipation projectParticipation = FakeProjectParticipations.PerformerSchneeköniginParticipation;
            _tokenAccessor.UserRoles.Returns(new List<string> { RoleNames.Performer });
            var expectedDto = new ProjectParticipationDto
            {
                Id = projectParticipation.Id,
                CommentByPerformerInner = projectParticipation.CommentByPerformerInner,
                CommentByStaffInner = projectParticipation.CommentByStaffInner,
                CommentTeam = null,
                InvitationStatus = null,
                InvitationStatusId = null,
                ParticipationStatusInternal = "Candidate",
                ParticipationStatusInternalId = projectParticipation.ParticipationStatusInternalId,
                ParticipationStatusInner = "Acceptance",
                ParticipationStatusInnerId = projectParticipation.ParticipationStatusInnerId,
                MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                Project = ReducedProjectDtoData.Schneekönigin,
                Person = null
            };

            // Act
            ProjectParticipationDto dto = _mapper.Map<ProjectParticipationDto>(projectParticipation);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
