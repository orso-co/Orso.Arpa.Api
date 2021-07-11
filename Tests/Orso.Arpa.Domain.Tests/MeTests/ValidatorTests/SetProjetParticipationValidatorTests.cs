using System;
using System.Linq.Expressions;
using System.Threading;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MeTests.ValidatorTests
{
    [TestFixture]
    public class SetProjetParticipationValidatorTests
    {
        private SetProjectParticipation.Validator _validator;
        private IArpaContext _arpaContext;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            DbSet<SelectValueCategory> mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(mockSelectValueCategoryDbSet);
            _validator = new SetProjectParticipation.Validator(_arpaContext) { CascadeMode = FluentValidation.CascadeMode.Stop };
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_PersonId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(command => command.PersonId, Guid.NewGuid(), nameof(Person));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_ProjectId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(command => command.ProjectId, Guid.NewGuid(), nameof(Project));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Completed_ProjectId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            Project project = ProjectSeedData.HoorayForHollywood;
            project.SetProperty(nameof(Project.IsCompleted), true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(project);
            _validator.ShouldHaveValidationErrorForExact(command => command.ProjectId, Guid.NewGuid())
                .WithErrorMessage("The project is completed. You may not set the participation of a completed project");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_StatusId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.HoorayForHollywood);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);

            _validator.ShouldThrowNotFoundExceptionFor(command => command.StatusId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Matching_StatusId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.HoorayForHollywood);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            _validator.ShouldHaveValidationErrorForExact(command => command.StatusId, Guid.NewGuid())
                .WithErrorMessage("The selected value is not valid for this field");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_MusicianProfileId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.HoorayForHollywood);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);

            _validator.ShouldThrowNotFoundExceptionFor(command => command.MusicianProfileId, new SetProjectParticipation.Command
            {
                MusicianProfileId = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                StatusId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id
            }, nameof(MusicianProfile));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Deativated_MusicianProfileId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.HoorayForHollywood);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.PerformersDeactivatedTubaProfile);

            _validator.ShouldHaveValidationErrorForExact(command => command.MusicianProfileId, new SetProjectParticipation.Command
            {
                MusicianProfileId = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                StatusId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id
            }).WithErrorMessage("Your musician profile is deactivated. A deactivated musician profile may not participate in a project");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Values_Are_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Project>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(ProjectSeedData.HoorayForHollywood);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<MusicianProfile>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(MusicianProfileSeedData.AdminMusicianSopranoProfile);

            _validator.ShouldNotHaveValidationErrorForExact(command => command.StatusId, new SetProjectParticipation.Command
            {
                MusicianProfileId = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                StatusId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[0].Id
            });
        }
    }
}
