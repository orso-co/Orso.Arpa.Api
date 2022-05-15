using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.MusicianProfileTests.ValidatorTests
{
    [TestFixture]
    public class MusicianProfileCreateValidatorTests
    {
        private Create.Validator _validator;
        private IArpaContext _arpaContext;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;
        private DbSet<Section> _mockSectionDbSet;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Create.Validator(_arpaContext);
            DbSet<MusicianProfile> mockMusicianProfiles = MockDbSets.MusicianProfiles;
            _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        #region PersonId
        [Test]
        public async Task Should_Have_Validation_Error_If_PersonId_Is_Not_Existing()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.PersonId, Guid.NewGuid(), nameof(Person));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_PersonId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.PersonId, Guid.Empty, nameof(Person));
        }
        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_PersonId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.PersonId, PersonTestSeedData.Performer.Id);
        }
        #endregion

        #region InstrumentId
        [Test]
        public async Task Should_Have_Validation_Error_If_InstrumentId_Is_Not_Existing()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InstrumentId, Guid.NewGuid(), nameof(Section));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InstrumentId, Guid.Empty, nameof(Section));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_MusicianProfile_Exists_With_InstrumentId()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            IEnumerable<ValidationFailure> errors = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.InstrumentId, SectionDtoData.Euphonium.Id);
            errors.First().ErrorMessage.Should().Be("There is already a musician profile for this person with this instrument id");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InstrumentId, SectionDtoData.Euphonium.Id);
        }
        #endregion

        #region QualificationId
        [Test]
        public async Task Should_Have_Validation_Error_If_QualificationId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.QualificationId, SelectValueMappingSeedData.MusicianProfileQualificationMappings[0].Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.Empty, nameof(SelectValueMapping));
        }
        #endregion

        #region InquiryStatusInnerId
        [Test]
        public async Task Should_Have_Validation_Error_If_InquiryStatusInnerId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InquiryStatusInnerId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_InquiryStatusInnerId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InquiryStatusInnerId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_InquiryStatusInnerId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InquiryStatusInnerId, SelectValueMappingSeedData.MusicianProfileInquiryStatusInnerMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_InquiryStatusInnerId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InquiryStatusInnerId, (Guid?)null);
        }
        #endregion

        #region InquiryStatusTeamId
        [Test]
        public async Task Should_Have_Validation_Error_If_InquiryStatusTeamId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InquiryStatusTeamId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_InquiryStatusTeamId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InquiryStatusTeamId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_InquiryStatusTeamId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InquiryStatusTeamId, SelectValueMappingSeedData.MusicianProfileInquiryStatusTeamMappings[0].Id);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_InquiryStatusTeamId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InquiryStatusTeamId, (Guid?)null);
        }
        #endregion

        #region PreferredPositionsTeam

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Staff_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new Create.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public async Task Should_Throw_NotFoundException_If_Preferred_Position_Staff_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            await _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsTeamIds, new Create.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Staff_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new Create.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        #endregion

        #region PreferredPositionsInner

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Performer_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new Create.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public async Task Should_Throw_NotFoundException_If_Preferred_Position_Performer_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            await _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsInnerIds, new Create.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Performer_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new Create.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        #endregion

        #region PreferredPartsTeam

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Part_Staff_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsTeam, new Create.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsTeam = new List<byte> { 8 }
            })).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Staff_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsTeam, new Create.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPartsTeam = new List<byte> { 2 }
            });
        }

        #endregion

        #region PreferredPartsInner

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Part_Performer_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new Create.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsInner = new List<byte> { 8 }
            })).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Performer_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new Create.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPartsInner = new List<byte> { 2 }
            });
        }

        #endregion
    }
}
