using System;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.ProjectParticipations;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Domain.Tests.ProjectParticipationsTests.ValidatorTests
{
    [TestFixture]
    public class GetForMusicianProfileValidatorTests
    {
        private IArpaContext _arpaContext;
        private GetForMusicianProfile.Validator _validator;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new GetForMusicianProfile.Validator(_arpaContext);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_Musician_Profile_Is_Suppliedf()
        {
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);

            _validator.ShouldHaveNotFoundErrorFor(c => c.MusicianProfileId, Guid.NewGuid(), nameof(MusicianProfile));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Musician_Profile_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            _validator.ShouldNotHaveValidationErrorForExact(c => c.MusicianProfileId, Guid.NewGuid());
        }
    }
}
