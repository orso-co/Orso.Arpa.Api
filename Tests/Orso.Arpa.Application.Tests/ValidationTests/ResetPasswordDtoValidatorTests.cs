using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
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
        public void Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied(
            [Values(null, "", "1234", "123456", "aaaaaa", "AAAAAA", "aaaAAA", "aaAA11", "%%%%%%")] string password)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Password, password);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Password, UserSeedData.ValidPassword);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.UsernameOrEmail, username);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Token_Is_Supplied([Values(null, "")] string token)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Token, token);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.UsernameOrEmail, "ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Token_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Token, "token");
        }
    }
}
