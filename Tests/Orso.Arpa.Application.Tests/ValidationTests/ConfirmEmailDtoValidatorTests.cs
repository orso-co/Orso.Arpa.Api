using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
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
        public void Should_Have_Validation_Error_If_Empty_Token_Is_Supplied([Values(null, "")] string givenName)
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.Token, givenName);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Token, "token");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.Email, email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Email, "ludmilla@test.com");
        }
    }
}
