using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ResetPasswordDtoValidatorTests
    {
        private ResetPasswordDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ResetPasswordDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied(
            [Values(null, "", "1234", "123456", "aaaaaa", "AAAAAA", "aaaAAA", "aaAA11", "%%%%%%")] string password)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Password, password);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Password, UserSeedData.ValidPassword);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.UsernameOrEmail, username);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Token_Is_Supplied([Values(null, "")] string token)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Token, token);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.UsernameOrEmail, "ludmilla");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Token, "token");
        }
    }
}
