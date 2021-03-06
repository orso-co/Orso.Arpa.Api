using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Resources.Cultures;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UserRegisterDtoValidatorTests
    {
        private UserRegisterDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<ApplicationResource>  localizer =
                new StringLocalizer<ApplicationResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _validator = new UserRegisterDtoValidator(localizer);
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

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ClientUri_Is_Supplied([Values(null, "", "http:/mw1.google.com", "foo/bar")] string clientUri)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.ClientUri, clientUri);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ClientUri_Is_Supplied([Values("http://localhost:4200", "https://www.google.de")] string clientUri)
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.ClientUri, clientUri);
        }
    }
}
