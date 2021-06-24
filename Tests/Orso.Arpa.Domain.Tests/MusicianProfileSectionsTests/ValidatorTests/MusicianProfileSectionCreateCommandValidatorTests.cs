using System;
using System.Linq;
using System.Threading;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfileSections;
using Orso.Arpa.Domain.Tests.Extensions;
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
        private ITokenAccessor _tokenAccessor;
        private DbSet<SelectValueCategory> _mockSelectValueCategoryDbSet;
        private DbSet<Section> _mockSectionDbSet;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _tokenAccessor = Substitute.For<ITokenAccessor>();
            _validator = new Create.Validator(_arpaContext, _tokenAccessor);
            DbSet<MusicianProfile> mockMusicianProfiles = MockDbSets.MusicianProfiles;
            _arpaContext.MusicianProfiles.Returns(mockMusicianProfiles);
            _mockSelectValueCategoryDbSet = MockDbSets.SelectValueCategories;
            _arpaContext.SelectValueCategories.Returns(_mockSelectValueCategoryDbSet);
            _mockSectionDbSet = MockDbSets.Sections;
            _arpaContext.Sections.Returns(_mockSectionDbSet);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied_Child_Instrument()
        {
            MusicianProfile profile = FakeMusicianProfiles.PerformerHornMusicianProfile;
            _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(profile, profile);
            _validator.ShouldNotHaveValidationErrorForExact(c => c.InstrumentId, new Create.Command
            {
                InstrumentId = SectionSeedData.WagnerTuba.Id,
                MusicianProfileId = profile.Id
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied_Instrument()
        {
            var profile = new MusicianProfile(new Logic.MusicianProfiles.Create.Command
            {
                InstrumentId = SectionSeedData.PiccoloFlute.Id
            }, true, Guid.NewGuid());
            profile.SetProperty(nameof(MusicianProfile.Instrument), FakeSections.Flute.Children.First());
            _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(profile, profile);
            _validator.ShouldNotHaveValidationErrorForExact(c => c.InstrumentId, new Create.Command
            {
                InstrumentId = SectionSeedData.Flute.Id,
                MusicianProfileId = profile.Id
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_DoublingInstrumentId_Is_MainInstrumentId()
        {
            _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(FakeMusicianProfiles.PerformerHornMusicianProfile);
            _validator.ShouldHaveValidationErrorForExact(c => c.InstrumentId, SectionSeedData.Horn.Id)
                .WithErrorMessage("The doubling instrument may not be the main instrument");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_DoublingInstrumentId_Is_Supplied()
        {
            MusicianProfile profile = FakeMusicianProfiles.PerformerHornMusicianProfile;
            _arpaContext.GetByIdAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(profile, profile);
            _validator.ShouldHaveValidationErrorForExact(c => c.InstrumentId, new Create.Command
            {
                MusicianProfileId = profile.Id,
                InstrumentId = SectionSeedData.Piano.Id
            })
                .WithErrorMessage("This instrument is no valid doubling instrument for the selected main instrument");
        }

        [Test]
        public void Should_Have_Validation_Error_If_AvailabilityId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.AvailabilityId, Guid.NewGuid(), nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_AvailabilityId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _validator.ShouldThrowNotFoundExceptionFor(c => c.AvailabilityId, SelectValueMappingSeedData.AddressTypeMappings[0].Id, nameof(SelectValueMapping));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_AvailabilityId_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<SelectValueMapping>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.FindAsync<Section>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeSections.Flute);
            _validator.ShouldNotHaveValidationErrorForExact(c => c.AvailabilityId, SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id);
        }
    }
}
