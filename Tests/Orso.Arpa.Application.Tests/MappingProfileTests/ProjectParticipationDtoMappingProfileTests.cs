using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Infrastructure.Localization;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ProjectParticipationDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            _ = services.AddSingleton<RoleBasedSetNullAction<ProjectParticipation, ProjectParticipationDto>>();
            _ = services.AddSingleton(_tokenAccessor);
            _ = services.AddSingleton(_localizerCache);
            _ = services.AddSingleton<LocalizeAction<MusicianProfile, ReducedMusicianProfileDto>>();
            _ = services.AddSingleton<RoleBasedSetNullAction<MusicianProfile, ReducedMusicianProfileDto>>();
            _ = services.AddSingleton<RoleBasedSetNullAction<Person, ReducedPersonDto>>();
            _ = services.AddAutoMapper(cfg =>
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
        private readonly ILocalizerCache _localizerCache = Substitute.For<ILocalizerCache>();

        [Test]
        public void Should_Map_For_Staff()
        {
            // Arrange
            ProjectParticipation projectParticipation = FakeProjectParticipations.PerformerSchneeköniginParticipation;
            projectParticipation.MusicianProfile.Person.SetProperty(nameof(Person.User), UserTestSeedData.Performer);
            _ = _tokenAccessor.GetUserRoles().Returns([RoleNames.Staff]);
            var expectedDto = new ProjectParticipationDto
            {
                Id = projectParticipation.Id,
                CommentByPerformerInner = projectParticipation.CommentByPerformerInner,
                CommentByStaffInner = projectParticipation.CommentByStaffInner,
                CommentTeam = projectParticipation.CommentTeam,
                InvitationStatus = projectParticipation.InvitationStatus,
                ParticipationStatusInternal = projectParticipation.ParticipationStatusInternal,
                ParticipationStatusInner = projectParticipation.ParticipationStatusInner,
                MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                Project = ReducedProjectDtoData.Schneekönigin,
                Person = ReducedPersonDtoData.Performer,
                ParticipationStatusResult = ProjectParticipationStatusResult.Pending
            };
            expectedDto.Person.UserEmail = UserTestSeedData.Performer.Email;

            // Act
            ProjectParticipationDto dto = _mapper.Map<ProjectParticipationDto>(projectParticipation);

            // Assert
            _ = dto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void Should_Map_For_Performer()
        {
            // Arrange
            ProjectParticipation projectParticipation = FakeProjectParticipations.PerformerSchneeköniginParticipation;
            projectParticipation.MusicianProfile.Person.SetProperty(nameof(Person.User), UserTestSeedData.Performer);
            _ = _tokenAccessor.GetUserRoles().Returns([RoleNames.Performer]);
            var expectedDto = new ProjectParticipationDto
            {
                Id = projectParticipation.Id,
                CommentByPerformerInner = projectParticipation.CommentByPerformerInner,
                CommentByStaffInner = projectParticipation.CommentByStaffInner,
                CommentTeam = null,
                InvitationStatus = null,
                ParticipationStatusInternal = projectParticipation.ParticipationStatusInternal,
                ParticipationStatusInner = projectParticipation.ParticipationStatusInner,
                MusicianProfile = ReducedMusicianProfileDtoData.PerformerProfile,
                Project = ReducedProjectDtoData.Schneekönigin,
                ParticipationStatusResult = ProjectParticipationStatusResult.Pending,
                Person = null,
            };
            expectedDto.MusicianProfile.PreferredPositionsTeam = null;

            // Act
            ProjectParticipationDto dto = _mapper.Map<ProjectParticipationDto>(projectParticipation);

            // Assert
            _ = dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
