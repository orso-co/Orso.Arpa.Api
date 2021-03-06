using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Resources.Cultures;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetRoleDtoValidatorTests
    {
        private SetRoleDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<ApplicationResource>  localizer =
                new StringLocalizer<ApplicationResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _validator = new SetRoleDtoValidator(localizer);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Username, username);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_RoleName_Is_Supplied([Values(null, "")] string roleName)
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoleNames, new[] { roleName });
        }
    }
}
