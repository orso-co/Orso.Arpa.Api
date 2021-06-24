using System;
using System.Collections.Generic;
using System.Threading;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Tests.Extensions;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MeTests.ValidatorTests
{
    [TestFixture]
    public class ModifyMusicianProfileValidatorTests
    {
        private ModifyMusicianProfile.Validator _validator;
        private IArpaContext _arpaContext;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;
        private DbSet<Section> _mockSectionDbSet;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new ModifyMusicianProfile.Validator(_arpaContext);
            DbSet<MusicianProfile> mockMusicianProfiles = MockDbSets.MusicianProfiles;
            _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        [Test]
        public void Should_Have_ValidationError_For_Empty_ExistingMusicianProfile()
        {
            _validator.ShouldHaveValidationErrorForExact(c => c.ExistingMusicianProfile, (MusicianProfile)null);
        }

        [Test]
        public void Should_Not_Have_ValidationError_For_Valid_ExistingMusicianProfile()
        {
            _validator.ShouldNotHaveValidationErrorForExact(
                c => c.ExistingMusicianProfile,
                new ModifyMusicianProfile.Command() { ExistingMusicianProfile = new MusicianProfile() });
        }

        [Test]
        public void Should_Have_ValidationError_If_IsMainProfile_Shall_Be_Turned_Off()
        {
            _validator.ShouldHaveValidationErrorForExact(
                c => c.IsMainProfile,
                new ModifyMusicianProfile.Command { IsMainProfile = false, ExistingMusicianProfile = MusicianProfileSeedData.AdminMusicianSopranoProfile })
                .WithErrorMessage("You may not turn off the IsMainProfile flag");
        }

        [Test]
        public void Should_Not_Have_ValidationError_If_IsMainProfile_Shall_Be_Turned_On()
        {
            _validator.ShouldNotHaveValidationErrorForExact(
                c => c.IsMainProfile,
                new ModifyMusicianProfile.Command { IsMainProfile = true, ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile });
        }

        [Test]
        public void Should_Not_Have_ValidationError_If_MainProfile_Is_Already_Off()
        {
            _validator.ShouldNotHaveValidationErrorForExact(
                c => c.IsMainProfile,
                new ModifyMusicianProfile.Command { IsMainProfile = false, ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_IncquiryStatusInner_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorForExact(c => c.InquiryStatusInnerId, SelectValueMappingSeedData.MusicianProfileInquiryStatusInnerMappings[0].Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Preferred_Position_Inner_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _validator.ShouldHaveValidationErrorForExact(cmd => cmd.PreferredPositionsInnerIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public void Should_Throw_NotFoundException_If_Preferred_Position_Inner_Does_Not_Exist()
        {
            _validator.ShouldThrowNotFoundExceptionFor(cmd => cmd.PreferredPositionsInnerIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Inner_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            _validator.ShouldNotHaveValidationErrorForExact(cmd => cmd.PreferredPositionsInnerIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Preferred_Part_Inner_Is_Supplied()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _validator.ShouldHaveValidationErrorForExact(cmd => cmd.PreferredPartsInner, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsInner = new List<byte> { 8 }
            }).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Inner_Is_Supplied()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            _validator.ShouldNotHaveValidationErrorForExact(cmd => cmd.PreferredPartsInner, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPartsInner = new List<byte> { 2 }
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_MusicianProfile_With_Active_ProjectParticipation_Shall_Be_Deactivated()
        {
            _validator.ShouldHaveValidationErrorForExact(c => c.IsDeactivated, new ModifyMusicianProfile.Command
            {
                ExistingMusicianProfile = FakeMusicianProfiles.PerformerMusicianProfile,
                IsDeactivated = true
            }).WithErrorMessage("You may not deactivate a musician profile which is participating in an active project");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_MusicianProfile_Without_ProjectParticipations_Shall_Be_Deactivated()
        {
            _validator.ShouldNotHaveValidationErrorForExact(c => c.IsDeactivated, new ModifyMusicianProfile.Command
            {
                ExistingMusicianProfile = MusicianProfileSeedData.PerformerMusicianProfile,
                IsDeactivated = true
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_MusicianProfile_Shall_Be_Reactivated()
        {
            _validator.ShouldNotHaveValidationErrorForExact(c => c.IsDeactivated, new ModifyMusicianProfile.Command
            {
                ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile,
                IsDeactivated = false
            });
        }
    }
}
