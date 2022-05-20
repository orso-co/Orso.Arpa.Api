using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class CreateEmailConfirmationTokenDtoValidatorTests
    {
        private CreateEmailConfirmationTokenDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateEmailConfirmationTokenDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.UsernameOrEmail, email);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.UsernameOrEmail, "ludmilla@test.com");
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
