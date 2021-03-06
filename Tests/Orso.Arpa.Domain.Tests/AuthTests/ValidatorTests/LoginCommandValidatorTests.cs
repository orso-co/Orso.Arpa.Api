using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Resources.Cultures;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.Login;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class LoginCommandValidatorTests
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
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new Command { UsernameOrEmail = "test", Password = UserSeedData.ValidPassword });

            act.Should().Throw<AuthenticationException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.UsernameOrEmail, UserSeedData.Performer.UserName);
        }
    }
}
