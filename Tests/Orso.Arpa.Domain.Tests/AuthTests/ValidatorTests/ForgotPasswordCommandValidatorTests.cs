using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Tests.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.ForgotPassword;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class ForgotPasswordCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new Validator(_userManager);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Username_Does_Not_Exist()
        {
            _validator.ShouldThrowNotFoundExceptionFor(c => c.UsernameOrEmail, "DoesNotExist");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.UsernameOrEmail, UserTestSeedData.Performer.UserName);
        }
    }
}
