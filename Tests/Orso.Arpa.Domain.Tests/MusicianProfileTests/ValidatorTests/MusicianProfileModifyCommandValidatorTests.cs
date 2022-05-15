using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MusicianProfileTests.ValidatorTests
{
    [TestFixture]
    public class MusicianProfileModifyValidatorTests
    {
        private Modify.Validator _validator;
        private IArpaContext _arpaContext;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;
        private DbSet<Section> _mockSectionDbSet;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Modify.Validator(_arpaContext);
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
            _validator.ShouldHaveValidationErrorForExactAsync(c => c.ExistingMusicianProfile, (MusicianProfile)null);
        }

        [Test]
        public void Should_Not_Have_ValidationError_For_Valid_ExistingMusicianProfile()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(
                c => c.ExistingMusicianProfile,
                new Modify.Command() { ExistingMusicianProfile = new MusicianProfile() });
        }

        [Test]
        public async Task Should_Have_ValidationError_If_IsMainProfile_Shall_Be_Turned_Off()
        {
            (await _validator.ShouldHaveValidationErrorForExactAsync(
                c => c.IsMainProfile,
                new Modify.Command { IsMainProfile = false, ExistingMusicianProfile = MusicianProfileSeedData.AdminMusicianSopranoProfile }))
                .WithErrorMessage("You may not turn off the IsMainProfile flag");
        }

        [Test]
        public void Should_Not_Have_ValidationError_If_IsMainProfile_Shall_Be_Turned_On()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(
                c => c.IsMainProfile,
                new Modify.Command { IsMainProfile = true, ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile });
        }

        [Test]
        public void Should_Not_Have_ValidationError_If_MainProfile_Is_Already_Off()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(
                c => c.IsMainProfile,
                new Modify.Command { IsMainProfile = false, ExistingMusicianProfile = MusicianProfileSeedData.PerformersDeactivatedTubaProfile });
        }

        [Test]
        public void Should_Have_Validation_Error_If_QualificationId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.QualificationId, SelectValueMappingSeedData.MusicianProfileQualificationMappings[0].Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_IncquiryStatusInner_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_IncquiryStatusInner_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_IncquiryStatusInner_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InquiryStatusInnerId, SelectValueMappingSeedData.MusicianProfileInquiryStatusInnerMappings[0].Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_IncquiryStatusTeam_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.InquiryStatusTeamId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_IncquiryStatusTeam_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.InquiryStatusTeamId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_IncquiryStatusTeam_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InquiryStatusTeamId, SelectValueMappingSeedData.MusicianProfileInquiryStatusTeamMappings[0].Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_SalaryId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.SalaryId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_SalaryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldHaveNotFoundErrorForAsync(c => c.SalaryId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SalaryId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryId, SelectValueMappingSeedData.MusicianProfileSalaryMappings[0].Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Inner_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new Modify.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public void Should_Throw_NotFoundException_If_Preferred_Position_Inner_Does_Not_Exist()
        {
            _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsInnerIds, new Modify.Command
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
            _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new Modify.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Team_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new Modify.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public void Should_Throw_NotFoundException_If_Preferred_Position_Team_Does_Not_Exist()
        {
            _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsTeamIds, new Modify.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Team_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new Modify.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Part_Inner_Is_Supplied()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new Modify.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsInner = new List<byte> { 8 }
            })).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Inner_Is_Supplied()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new Modify.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPartsInner = new List<byte> { 2 }
            });
        }
    }
}
