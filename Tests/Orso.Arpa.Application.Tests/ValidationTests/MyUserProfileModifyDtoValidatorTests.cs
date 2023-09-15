using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MyUserProfileModifyDtoValidatorTests
    {
        private MyUserProfileModifyDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MyUserProfileModifyDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Email, email);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Email, "ludmilla@test.com");
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_GivenName_Is_Supplied([Values(null, "")] string givenName)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.GivenName, givenName);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Surname_Is_Supplied([Values(null, "")] string surname)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Surname, surname);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Surname, "Schneider");
        }
    }
}
