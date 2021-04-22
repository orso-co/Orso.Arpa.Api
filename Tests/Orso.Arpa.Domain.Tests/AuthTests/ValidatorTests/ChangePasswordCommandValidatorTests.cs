using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Identity;
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
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _validator = new Validator(_userManager, _userAccessor);
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