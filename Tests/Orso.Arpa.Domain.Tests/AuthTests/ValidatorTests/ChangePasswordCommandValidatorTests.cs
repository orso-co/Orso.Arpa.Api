using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.ChangePassword;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class ChangePasswordCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _validator = new Validator(_userManager, _userAccessor, localizer);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Wrong_Current_Password_Is_Supplied()
        {
            _userAccessor.GetCurrentUserAsync().Returns(Arpa.Tests.Shared.FakeData.FakeUsers.Performer);
            _validator.ShouldHaveValidationErrorFor(command => command.CurrentPassword, "WrongPassword");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Correct_Current_Password_Is_Supplied()
        {
            _userAccessor.GetCurrentUserAsync().Returns(Arpa.Tests.Shared.FakeData.FakeUsers.Performer);
            _validator.ShouldNotHaveValidationErrorFor(command => command.CurrentPassword, UserSeedData.ValidPassword);
        }
    }
}
