using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.CreateEmailConfirmationToken;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class CreateEmailConfirmationTokenCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _userManager = new FakeUserManager();
            _validator = new Validator(_userManager, localizer);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Email_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.UsernameOrEmail, "DoesNotExist");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.UsernameOrEmail, UserTestSeedData.Performer.Email);
        }
    }
}
