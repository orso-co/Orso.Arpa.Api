using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Me.Modify;

namespace Orso.Arpa.Domain.Tests.MeTests.ValidatorTests
{
    [TestFixture]
    public class UserProfileModifyCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _validator = new Validator(_userManager, _userAccessor);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Email_Does_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Email, UserSeedData.Performer.Email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Email, "ludmilla@test.com");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Surname, "Schneider");
        }
    }
}
