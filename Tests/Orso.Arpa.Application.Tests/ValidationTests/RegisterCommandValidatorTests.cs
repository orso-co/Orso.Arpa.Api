using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RegisterCommandValidatorTests
    {

        private Register.CommandValidator _validator;
        private UserManager<User> _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new Register.CommandValidator(_userManager);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Email, email);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Email_Does_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Email, UserSeedData.Egon.Email);
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
        public void Should_Have_Validation_Error_If_Empty_Username_Is_Supplied([Values(null, "")] string username)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Username, username);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Username_Does_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Username, UserSeedData.Egon.UserName);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Username_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Username, "ludmilla");
        }
    }
}
