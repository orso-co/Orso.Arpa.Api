using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class CreateEmailConfirmationTokenDtoValidatorTests
    {
        private CreateEmailConfirmationTokenDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<ApplicationResource> localizer =
                new StringLocalizer<ApplicationResource>(
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _validator = new CreateEmailConfirmationTokenDtoValidator(localizer);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.UsernameOrEmail, email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.UsernameOrEmail, "ludmilla@test.com");
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
