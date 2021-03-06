using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SendQRCodeCommandValidatorTests
    {
        private SendQRCode.Validator _validator;
        private ArpaUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new SendQRCode.Validator(_userManager);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Username_Does_Exist()
        {
            _validator.ShouldNotHaveValidationErrorForExact(c => c.Username, UserTestSeedData.Performer.UserName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Username_Does_Not_Exist()
        {
            _validator.ShouldHaveNotFoundErrorFor(c => c.Username, "DoesNotExist", typeof(User).Name);
        }
    }
}
