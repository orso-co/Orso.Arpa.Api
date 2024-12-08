using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MusicianProfileTests.ValidatorTests
{
    [TestFixture]
    public class MusicianProfileModifyValidatorTests
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
            _ = _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _ = _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _ = _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        [Test]
        public async Task Should_Have_ValidationError_For_Empty_ExistingMusicianProfile()
        {
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.ExistingMusicianProfile, (MusicianProfile)null);
        }

        [Test]
        public async Task Should_Not_Have_ValidationError_For_Valid_ExistingMusicianProfile()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(
                c => c.ExistingMusicianProfile,
                new ModifyMusicianProfile.Command() { ExistingMusicianProfile = new MusicianProfile() });
        }

        [Test]
        public async Task Should_Have_ValidationError_If_IsMainProfile_Shall_Be_Turned_Off()
        {
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(
                c => c.IsMainProfile,
                new ModifyMusicianProfile.Command { IsMainProfile = false, ExistingMusicianProfile = MusicianProfileSeedData.AdminMusicianSopranoProfile }))
                .WithErrorMessage("You may not turn off the IsMainProfile flag");
        }

        [Test]
        public async Task Should_Not_Have_ValidationError_If_IsMainProfile_Shall_Be_Turned_On()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(
                c => c.IsMainProfile,
                new ModifyMusicianProfile.Command { IsMainProfile = true, ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile });
        }

        [Test]
        public async Task Should_Not_Have_ValidationError_If_MainProfile_Is_Already_Off()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(
                c => c.IsMainProfile,
                new ModifyMusicianProfile.Command { IsMainProfile = false, ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_QualificationId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_QualificationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.QualificationId, SelectValueMappingSeedData.MusicianProfileQualificationMappings[0].Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_IncquiryStatusInner_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_IncquiryStatusInner_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_SalaryId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.SalaryId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_SalaryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.SalaryId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_SalaryId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryId, SelectValueMappingSeedData.MusicianProfileSalaryMappings[0].Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Inner_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = [SelectValueSectionSeedData.ClarinetCoach.Id]
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public async Task Should_Throw_NotFoundException_If_Preferred_Position_Inner_Does_Not_Exist()
        {
            await _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsInnerIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = [SelectValueSectionSeedData.ClarinetCoach.Id]
            }, nameof(SelectValueSection));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Inner_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsInnerIds = [SelectValueSectionSeedData.HornLow.Id]
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Team_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = [SelectValueSectionSeedData.ClarinetCoach.Id]
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public async Task Should_Throw_NotFoundException_If_Preferred_Position_Team_Does_Not_Exist()
        {
            await _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsTeamIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = [SelectValueSectionSeedData.ClarinetCoach.Id]
            }, nameof(SelectValueSection));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Team_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsTeamIds = [SelectValueSectionSeedData.HornLow.Id]
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Part_Inner_Is_Supplied()
        {
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsInner = [8]
            })).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Inner_Is_Supplied()
        {
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new ModifyMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPartsInner = [2]
            });
        }
    }
}
