using System.Threading.Tasks;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.Login;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class LoginCommandValidatorTests
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
        public async Task Should_Have_Validation_Error_If_Email_Does_Not_Exist()
        {
            TestValidationResult<Command> result = await _validator
                .TestValidateAsync(new Command { UsernameOrEmail = "test", Password = UserSeedData.ValidPassword });

            result.ShouldHaveValidationErrorFor(c => c.UsernameOrEmail)
                .WithErrorCode("401")
                .WithErrorMessage("The system could not log you in. Please enter a valid user name and password");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(query => query.UsernameOrEmail, UserTestSeedData.Performer.UserName);
        }
    }
}
