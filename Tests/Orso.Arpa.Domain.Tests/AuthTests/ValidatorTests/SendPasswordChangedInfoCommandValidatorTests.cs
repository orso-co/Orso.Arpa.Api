using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SendPasswordChangedInfoCommandValidatorTests
    {
        private SendPasswordChangedInfo.Validator _validator;
        private ArpaUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new SendPasswordChangedInfo.Validator(_userManager);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Username_Does_Exist()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.UsernameOrEmail, UserTestSeedData.Performer.UserName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Username_Does_Not_Exist()
        {
            _validator.ShouldThrowNotFoundExceptionFor(c => c.UsernameOrEmail, "DoesNotExist", typeof(User).Name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Username_Is_Empty([Values(null, "")] string username)
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.UsernameOrEmail, username);
        }
    }
}
