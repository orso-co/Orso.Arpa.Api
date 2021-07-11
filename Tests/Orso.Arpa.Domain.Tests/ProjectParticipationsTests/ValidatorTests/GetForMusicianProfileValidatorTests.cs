using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.ProjectParticipations;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Domain.Tests.ProjectParticipationsTests.ValidatorTests
{
    [TestFixture]
    public class GetForMusicianProfileValidatorTests
    {
        private IArpaContext _arpaContext;
        private ITokenAccessor _tokenAccessor;
        private GetForMusicianProfile.Validator _validator;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _tokenAccessor = Substitute.For<ITokenAccessor>();
            _validator = new GetForMusicianProfile.Validator(_arpaContext, _tokenAccessor);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_Musician_Profile_Is_Supplied_By_Staff()
        {
            _tokenAccessor.UserRoles.Returns(new List<string> { RoleNames.Staff });
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);

            _validator.ShouldThrowNotFoundExceptionFor(c => c.MusicianProfileId, Guid.NewGuid(), nameof(MusicianProfile));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Musician_Profile_Is_Supplied_By_Staff()
        {
            _tokenAccessor.UserRoles.Returns(new List<string> { RoleNames.Staff });
            _arpaContext.EntityExistsAsync<MusicianProfile>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            _validator.ShouldNotHaveValidationErrorForExact(c => c.MusicianProfileId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Not_Existing_Musician_Profile_Is_Supplied_By_Performer()
        {
            _tokenAccessor.UserRoles.Returns(new List<string> { RoleNames.Performer });
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);

            Func<TestValidationResult<GetForMusicianProfile.Query>> testValidationResultFunction = () => _validator.TestValidate(new GetForMusicianProfile.Query { MusicianProfileId = Guid.NewGuid() });
            testValidationResultFunction.Should().ThrowExactly<AuthorizationException>()
                .WithMessage("This musician profile is not yours. You don't have access to this musician profile.");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Musician_Profile_Is_Supplied_By_Performer()
        {
            _tokenAccessor.UserRoles.Returns(new List<string> { RoleNames.Performer });
            _arpaContext.EntityExistsAsync(Arg.Any<Expression<Func<MusicianProfile, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);

            _validator.ShouldNotHaveValidationErrorForExact(c => c.MusicianProfileId, Guid.NewGuid());
        }
    }
}
