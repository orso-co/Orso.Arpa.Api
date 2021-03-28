using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Tests.Shared.Identity;
using static Orso.Arpa.Domain.Logic.Auth.SetRole;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SetRoleCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
        private RoleManager<Role> _roleManager;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<DomainResource> localizer =
                new StringLocalizer<DomainResource>(
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _userManager = new FakeUserManager();
            _roleManager = new FakeRoleManager();
            _validator = new Validator(_userManager, _roleManager, localizer);
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_Role_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.RoleNames, new[] { "DoesNotExist" });
        }

        [Test]
        public void Should_Have_Validation_Error_If_User_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Username, "DoesNotExist");
        }
    }
}
