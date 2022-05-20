using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UserRegisterDtoValidatorTests
    {
        private UserRegisterDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UserRegisterDtoValidator();
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
        public async Task Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied(
            [Values(null, "", "1234", "123456", "aaaaaa", "AAAAAA", "aaaAAA", "aaAA11", "%%%%%%")] string password)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Password, password);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Password, UserSeedData.ValidPassword);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.UserName, username);
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
        public async Task Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.UserName, "ludmilla");
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

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_ClientUri_Is_Supplied([Values(null, "", "http:/mw1.google.com", "foo/bar")] string clientUri)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(query => query.ClientUri, clientUri);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ClientUri_Is_Supplied([Values("http://localhost:4200", "https://www.google.de")] string clientUri)
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(query => query.ClientUri, clientUri);
        }
    }
}
