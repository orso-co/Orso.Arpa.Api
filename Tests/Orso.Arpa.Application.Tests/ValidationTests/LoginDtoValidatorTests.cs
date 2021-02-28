using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Resources.Cultures;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class LoginDtoValidatorTests
    {
        private LoginDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<ApplicationValidators> localizer =
                new StringLocalizer<ApplicationValidators>(
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _validator = new LoginDtoValidator(localizer);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_UserName_Is_Supplied([Values(null, "")] string userName)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.UsernameOrEmail, userName);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.UsernameOrEmail, UserTestSeedData.Performer.UserName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied([Values(null, "")] string password)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.Password, password);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.Password, UserSeedData.ValidPassword);
        }
    }
}
