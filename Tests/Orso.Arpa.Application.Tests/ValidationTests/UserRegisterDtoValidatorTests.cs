using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

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
            _validator.ShouldHaveValidationErrorForExact(command => command.Email, email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Email, "ludmilla@test.com");
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
            _validator.ShouldHaveValidationErrorForExact(command => command.UserName, username);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_GivenName_Is_Supplied([Values(null, "")] string givenName)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.GivenName, givenName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Surname_Is_Supplied([Values(null, "")] string surname)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Surname, surname);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.UserName, "ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Surname, "Schneider");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ClientUri_Is_Supplied([Values(null, "", "http:/mw1.google.com", "foo/bar")] string clientUri)
        {
            _validator.ShouldHaveValidationErrorForExact(query => query.ClientUri, clientUri);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ClientUri_Is_Supplied([Values("http://localhost:4200", "https://www.google.de")] string clientUri)
        {
            _validator.ShouldNotHaveValidationErrorForExact(query => query.ClientUri, clientUri);
        }
    }
}
