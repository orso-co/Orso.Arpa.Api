using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ConfirmEmailDtoValidatorTests
    {
        private ConfirmEmailDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ConfirmEmailDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Token_Is_Supplied([Values(null, "")] string givenName)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Token, givenName);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Token, "token");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Email, email);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Email, "ludmilla@test.com");
        }
    }
}
