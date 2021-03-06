using System.Collections.Generic;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Application.Resources.Cultures;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ChangePasswordDtoValidatorTests
    {
        private ChangePasswordDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<ApplicationResource>  localizer =
                new StringLocalizer<ApplicationResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            IEnumerable<LocalizedString> localizedStrings = localizer.GetAllStrings(true);
            _validator = new ChangePasswordDtoValidator(localizer);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_New_Password_Is_Supplied(
            [Values(null, "", "1234", "123456", "aaaaaa", "AAAAAA", "aaaAAA", "aaAA11", "%%%%%%")] string password)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.NewPassword, password);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_New_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.NewPassword, UserSeedData.ValidPassword);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Current_Password_Is_Supplied([Values(null, "")]string currentPassword)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.CurrentPassword, currentPassword);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Correct_Current_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.CurrentPassword, UserSeedData.ValidPassword);
        }
    }
}
