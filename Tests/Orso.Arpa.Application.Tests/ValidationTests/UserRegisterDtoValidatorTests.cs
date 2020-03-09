using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Application.AuthApplication.UserRegisterDto;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UserRegisterDtoValidatorTests
    {
        private UserRegisterDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UserRegisterDtoValidator();
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

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied(
            [Values(null, "", "1234", "123456", "aaaaaa", "AAAAAA", "aaaAAA", "aaAA11", "%%%%%%")] string password)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Password, password);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Password, UserSeedData.ValidPassword);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.UserName, username);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_GivenName_Is_Supplied([Values(null, "")] string givenName)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.GivenName, givenName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Surname_Is_Supplied([Values(null, "")] string surname)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Surname, surname);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.UserName, "ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Surname, "Schneider");
        }
    }
}
