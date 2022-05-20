using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Projects;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.ProjectTests.ValidatorTests
{
    [TestFixture]
    public class SetProjectParticipationCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private IDateTimeProvider _dateTimeProvider;
        private SetProjectParticipation.Validator _validator;
        private DbSet<Project> _mockProjectDbSet;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _dateTimeProvider = new FakeDateTimeProvider();
            _validator = new SetProjectParticipation.Validator(_arpaContext, _dateTimeProvider);
            _mockProjectDbSet = MockDbSets.Projects;
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _arpaContext.Projects.Returns(_mockProjectDbSet);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Project_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);

            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.ProjectId, Guid.Empty, nameof(Project));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Project_Is_Completed()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.RockingXMas);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.ProjectId, Guid.Empty))
                .WithErrorMessage("The project is completed. You may not set the participation of a completed project");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_ParticipationStatusInner_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.ParticipationStatusInnerId, Guid.Empty, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_ParticipationStatusInner_Does_Not_Match()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.ParticipationStatusInnerId, SelectValueMappingSeedData.ProjectParticipationStatusInternalMappings[0].Id))
                .WithErrorMessage("The selected value is not valid for this field");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_ParticipationStatusInternal_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.ParticipationStatusInternalId, Guid.Empty, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_ParticipationStatusInternal_Does_Not_Match()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.ParticipationStatusInternalId, SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id))
                .WithErrorMessage("The selected value is not valid for this field");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_InvitationStatus_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InvitationStatusId, Guid.Empty, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_InvitationStatus_Does_Not_Match()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.InvitationStatusId, SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id))
                .WithErrorMessage("The selected value is not valid for this field");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_MusicianProfile_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.MusicianProfileId, Guid.Empty, nameof(MusicianProfile));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_MusicianProfile_Is_Deactivated()
        {
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfileDeactivation, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.MusicianProfileId, Guid.Empty))
                .WithErrorMessage("The musician profile is deactivated. A deactivated musician profile may not participate in a project");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Data_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.Schneekönigin);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);

            await _validator.ShouldNotHaveValidationErrorForExactAsync(v => v.MusicianProfileId, new SetProjectParticipation.Command
            {
                CommentByStaffInner = "Comment1",
                CommentTeam = "Comment2",
                InvitationStatusId = SelectValueMappingSeedData.ProjectParticipationInvitationStatusMappings[0].Id,
                MusicianProfileId = MusicianProfileSeedData.AdminMusicianSopranoProfile.Id,
                ParticipationStatusInnerId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id,
                ParticipationStatusInternalId = SelectValueMappingSeedData.ProjectParticipationStatusInternalMappings[0].Id,
                ProjectId = ProjectSeedData.Schneekönigin.Id
            });
        }
    }
}
