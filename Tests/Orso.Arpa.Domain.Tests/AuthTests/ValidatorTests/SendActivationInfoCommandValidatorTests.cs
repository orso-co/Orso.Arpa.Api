using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SendActivationInfoCommandValidatorTests
    {
        private SendActivationInfo.Validator _validator;
        private ArpaUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new SendActivationInfo.Validator(_userManager);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Username_Does_Exist()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Username, UserSeedData.Performer.UserName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Username_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Username, "DoesNotExist");
        }
    }
}
