using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfileSections;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.MusicianProfileTests.ValidatorTests
{
    [TestFixture]
    public class MusicianProfileSectionsCreateValidatorTests
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
            _ = _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _ = _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _ = _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied_Child_Instrument()
        {
            MusicianProfile profile = FakeMusicianProfiles.PerformerHornMusicianProfile;
            _ = _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(profile, profile);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InstrumentId, new Create.Command
            {
                InstrumentId = SectionSeedData.WagnerTuba.Id,
                MusicianProfileId = profile.Id
            });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied_Instrument()
        {
            var profile = new MusicianProfile(new Domain.Logic.MusicianProfiles.Create.Command
            {
                InstrumentId = SectionSeedData.PiccoloFlute.Id
            }, true, Guid.NewGuid());
            profile.SetProperty(nameof(MusicianProfile.Instrument), FakeSections.Flute.Children.First());
            _ = _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(profile, profile);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.InstrumentId, new Create.Command
            {
                InstrumentId = SectionSeedData.Flute.Id,
                MusicianProfileId = profile.Id
            });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_DoublingInstrumentId_Is_MainInstrumentId()
        {
            _ = _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(FakeMusicianProfiles.PerformerHornMusicianProfile);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.InstrumentId, SectionSeedData.Horn.Id))
                .WithErrorMessage("The doubling instrument may not be the main instrument");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_DoublingInstrumentId_Is_Supplied()
        {
            MusicianProfile profile = FakeMusicianProfiles.PerformerHornMusicianProfile;
            _ = _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(profile, profile);
            _ = (await _validator.ShouldHaveValidationErrorForExactAsync(c => c.InstrumentId, new Create.Command
            {
                MusicianProfileId = profile.Id,
                InstrumentId = SectionSeedData.Piano.Id
            })).WithErrorMessage("This instrument is no valid doubling instrument for the selected main instrument");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_AvailabilityId_Does_Not_Exist()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.AvailabilityId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_AvailabilityId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.AvailabilityId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_AvailabilityId_Is_Supplied()
        {
            _ = _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _ = _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.AvailabilityId, SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id);
        }
    }
}
