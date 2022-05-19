using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ChangePasswordDtoValidatorTests
    {
        private ChangePasswordDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ChangePasswordDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_New_Password_Is_Supplied(
            [Values(null, "", "1234", "123456", "aaaaaa", "AAAAAA", "aaaAAA", "aaAA11", "%%%%%%")] string password)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.NewPassword, password);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_New_Password_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.NewPassword, UserSeedData.ValidPassword);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Current_Password_Is_Supplied([Values(null, "")] string currentPassword)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.CurrentPassword, currentPassword);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Correct_Current_Password_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.CurrentPassword, UserSeedData.ValidPassword);
        }
    }
}
