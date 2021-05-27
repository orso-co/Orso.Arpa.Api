using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Tests.Extensions;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Create;

namespace Orso.Arpa.Domain.Tests.MusicianProfileTests.ValidatorTests
{
    [TestFixture]
    public class MusicianProfileCreatecValidatorTests
    {
        private Validator _validator;
        private DoublingInstrumentValidator _doublingInstrumentValidator;
        private IArpaContext _arpaContext;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;
        private DbSet<Section> _mockSectionDbSet;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _doublingInstrumentValidator = new DoublingInstrumentValidator(_arpaContext);
            DbSet<MusicianProfile> mockMusicianProfiles = MockDbSets.MusicianProfiles;
            _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        #region PersonId
        [Test]
        public void Should_Have_Validation_Error_If_PersonId_Is_Not_Existing()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.PersonId, Guid.NewGuid(), nameof(Person));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_PersonId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.PersonId, Guid.Empty, nameof(Person));
        }
        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_PersonId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.PersonId, PersonTestSeedData.Performer.Id);
        }
        #endregion

        #region InstrumentId
        [Test]
        public void Should_Have_Validation_Error_If_InstrumentId_Is_Not_Existing()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.InstrumentId, Guid.NewGuid(), nameof(Section));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.InstrumentId, Guid.Empty, nameof(Section));
        }

        [Test]
        public void Should_Have_Validation_Error_If_MusicianProfile_Exists_With_InstrumentId()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            IEnumerable<ValidationFailure> errors = _validator.ShouldHaveValidationErrorFor(c => c.InstrumentId, SectionDtoData.Euphonium.Id);
            errors.First().ErrorMessage.Should().Be("There is already a musician profile for this person with this instrument id");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldNotHaveValidationErrorFor(c => c.InstrumentId, SectionDtoData.Euphonium.Id);
        }
        #endregion

        #region QualificationId
        [Test]
        public void Should_Have_Validation_Error_If_QualificationId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.QualificationId, SelectValueMappingSeedData.MusicianProfileQualificationMappings[0].Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_QualificationId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.QualificationId, Guid.Empty, nameof(SelectValueMapping));
        }
        #endregion

        #region InquiryStatusPerformerId
        [Test]
        public void Should_Have_Validation_Error_If_InquiryStatusPerformerId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.InquiryStatusPerformerId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_InquiryStatusPerformerId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.InquiryStatusPerformerId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InquiryStatusPerformerId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.InquiryStatusPerformerId, SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_InquiryStatusPerformerId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.InquiryStatusPerformerId, (Guid?)null);
        }
        #endregion

        #region InquiryStatusStaffId
        [Test]
        public void Should_Have_Validation_Error_If_InquiryStatusStaffId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.InquiryStatusStaffId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_InquiryStatusStaffId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.InquiryStatusStaffId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InquiryStatusStaffId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.InquiryStatusStaffId, SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[0].Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_InquiryStatusStaffId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _validator.ShouldNotHaveValidationErrorFor(c => c.InquiryStatusStaffId, (Guid?)null);
        }
        #endregion

        #region AvailabilityId

        [Test]
        public void Should_Have_Validation_Error_If_AvailabilityId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _doublingInstrumentValidator.MainInstrumentId = Guid.Empty;
            _doublingInstrumentValidator.ShouldThrowNotFoundExceptionFor(c => c.AvailabilityId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_AvailabilityId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _doublingInstrumentValidator.MainInstrumentId = Guid.Empty;
            _doublingInstrumentValidator.ShouldThrowNotFoundExceptionFor(c => c.AvailabilityId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_AvailabilityId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _doublingInstrumentValidator.MainInstrumentId = Guid.Empty;
            _doublingInstrumentValidator.ShouldNotHaveValidationErrorFor(c => c.AvailabilityId, SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id);
        }

        #endregion

        #region InstrumentId

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied_Case_1()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _doublingInstrumentValidator.MainInstrumentId = FakeSections.Flute.Id;
            _doublingInstrumentValidator.ShouldNotHaveValidationErrorFor(c => c.InstrumentId, FakeSections.Flute.Children.First().Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied_Case_2()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute.Children.First());
            _doublingInstrumentValidator.MainInstrumentId = FakeSections.Flute.Children.First().Id;
            _doublingInstrumentValidator.ShouldNotHaveValidationErrorFor(c => c.InstrumentId, FakeSections.Flute.Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_DoublingInstrumentId_Is_MainInstrumentId()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _doublingInstrumentValidator.MainInstrumentId = FakeSections.Flute.Id;
            _doublingInstrumentValidator.ShouldHaveValidationErrorFor(c => c.InstrumentId, FakeSections.Flute.Id)
                .WithErrorMessage("The doubling instrument may not be the main instrument");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_DoublingInstrumentId_Is_Supplied()
        {
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _doublingInstrumentValidator.MainInstrumentId = FakeSections.Flute.Id;
            _doublingInstrumentValidator.ShouldHaveValidationErrorFor(c => c.InstrumentId, SectionSeedData.Piano.Id)
                .WithErrorMessage("This instrument is no valid doubling instrument for the selected main instrument");
        }

        #endregion

        #region PreferredPositionsStaff

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Preferred_Position_Staff_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.PreferredPositionsStaffIds, new Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsStaffIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public void Should_Throw_NotFoundException_If_Preferred_Position_Staff_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _validator.ShouldThrowNotFoundExceptionFor(cmd => cmd.PreferredPositionsStaffIds, new Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsStaffIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Staff_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.PreferredPositionsStaffIds, new Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsStaffIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        #endregion

        #region PreferredPositionsPerformer

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Preferred_Position_Performer_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _validator.ShouldHaveValidationErrorFor(cmd => cmd.PreferredPositionsPerformerIds, new Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsPerformerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public void Should_Throw_NotFoundException_If_Preferred_Position_Performer_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _validator.ShouldThrowNotFoundExceptionFor(cmd => cmd.PreferredPositionsPerformerIds, new Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsPerformerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Performer_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            _validator.ShouldNotHaveValidationErrorFor(cmd => cmd.PreferredPositionsPerformerIds, new Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPositionsPerformerIds = new List<Guid> { SelectValueSectionSeedData.HornLow.Id }
            });
        }

        #endregion
    }
}
