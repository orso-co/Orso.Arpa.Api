using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Application.Tests.AuthTests.ValidationTests
{
    [TestFixture]
    public class ChangePasswordCommandValidatorTests
    {

        private ChangePassword.CommandValidator _validator;
        private UserManager<User> _userManager;
        private IUserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _validator = new ChangePassword.CommandValidator(_userManager, _userAccessor);
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
            _userAccessor.GetCurrentUserAsync().Returns(UserSeedData.Egon);
            _validator.ShouldHaveValidationErrorFor(command => command.CurrentPassword, currentPassword);
        }

        [Test]
        public void Should_Have_Validation_Error_If_User_Does_Not_Exist()
        {
            _userAccessor.GetCurrentUserAsync().Returns(default(User));
            _validator.ShouldHaveValidationErrorFor(command => command.CurrentPassword, UserSeedData.ValidPassword);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Wrong_Current_Password_Is_Supplied()
        {
            _userAccessor.GetCurrentUserAsync().Returns(UserSeedData.Egon);
            _validator.ShouldHaveValidationErrorFor(command => command.CurrentPassword, "WrongPassword");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Correct_Current_Password_Is_Supplied()
        {
            _userAccessor.GetCurrentUserAsync().Returns(UserSeedData.Egon);
            _validator.ShouldNotHaveValidationErrorFor(command => command.CurrentPassword, UserSeedData.ValidPassword);
        }
    }
}
