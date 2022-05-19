using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class LoginDtoValidatorTests
    {
        private LoginDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new LoginDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_UserName_Is_Supplied([Values(null, "")] string userName)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(query => query.UsernameOrEmail, userName);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(query => query.UsernameOrEmail, UserTestSeedData.Performer.UserName);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied([Values(null, "")] string password)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(query => query.Password, password);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(query => query.Password, UserSeedData.ValidPassword);
        }
    }
}
