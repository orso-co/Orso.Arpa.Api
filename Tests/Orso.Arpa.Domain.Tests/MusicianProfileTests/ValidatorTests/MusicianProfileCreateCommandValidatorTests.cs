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
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
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
        private CreateMusicianProfile.Validator _validator;
        private IArpaContext _arpaContext;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;
        private DbSet<Section> _mockSectionDbSet;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new CreateMusicianProfile.Validator(_arpaContext);
            DbSet<MusicianProfile> mockMusicianProfiles = MockDbSets.MusicianProfiles;
            _ = _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _ = _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _ = _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        #region PersonId
        [Test]
        public async Task Should_Have_Validation_Error_If_PersonId_Is_Not_Existing()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.PersonId, Guid.NewGuid(), nameof(Person));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_PersonId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.PersonId, Guid.Empty, nameof(Person));
        }
        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_PersonId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.PersonId, PersonTestSeedData.Performer.Id);
        }
        #endregion

        #region InstrumentId
        [Test]
        public async Task Should_Have_Validation_Error_If_InstrumentId_Is_Not_Existing()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InstrumentId, Guid.NewGuid(), nameof(Section));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.InstrumentId, Guid.Empty, nameof(Section));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_MusicianProfile_Exists_With_InstrumentId()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            IEnumerable<ValidationFailure> errors = await _validator.ShouldHaveValidationErrorForExactAsync(c => c.InstrumentId, SectionDtoData.Euphonium.Id);
            _ = errors.First().ErrorMessage.Should().Be("There is already a musician profile for this person with this instrument id");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InstrumentId, SectionDtoData.Euphonium.Id);
        }
        #endregion

        #region QualificationId
        [Test]
        public async Task Should_Have_Validation_Error_If_QualificationId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_QualificationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.QualificationId, SelectValueMappingSeedData.MusicianProfileQualificationMappings[0].Id);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_QualificationId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.QualificationId, Guid.Empty, nameof(SelectValueMapping));
        }
        #endregion

        #region PreferredPositionsTeam

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Preferred_Position_Staff_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public async Task Should_Throw_NotFoundException_If_Preferred_Position_Staff_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            await _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsTeamIds, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsTeamIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Staff_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsTeamIds, new CreateMusicianProfile.Command
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
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            })).WithErrorMessage("The selected position is not valid for this instrument");
        }

        [Test]
        public async Task Should_Throw_NotFoundException_If_Preferred_Position_Performer_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            await _validator.ShouldHaveNotFoundErrorFor(cmd => cmd.PreferredPositionsInnerIds, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPositionsInnerIds = new List<Guid> { SelectValueSectionSeedData.ClarinetCoach.Id }
            }, nameof(SelectValueSection));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Position_Performer_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<SelectValueSection>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPositionsInnerIds, new CreateMusicianProfile.Command
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
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsTeam, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsTeam = new List<byte> { 8 }
            })).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Staff_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsTeam, new CreateMusicianProfile.Command
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
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Accordion);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Accordion.Id,
                PreferredPartsInner = new List<byte> { 8 }
            })).WithErrorMessage("The selected part is not valid for this instrument");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Preferred_Part_Performer_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<Person>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.EntityExistsAsync<Section>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(SectionSeedData.Horn);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(cmd => cmd.PreferredPartsInner, new CreateMusicianProfile.Command
            {
                InstrumentId = SectionSeedData.Horn.Id,
                PreferredPartsInner = new List<byte> { 2 }
            });
        }

        #endregion
    }
}
