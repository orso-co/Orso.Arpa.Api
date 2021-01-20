using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;

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
            _validator.ShouldHaveValidationErrorFor(command => command.Token, givenName);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Token, "token");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Email, email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Email, "ludmilla@test.com");
        }
    }
}
