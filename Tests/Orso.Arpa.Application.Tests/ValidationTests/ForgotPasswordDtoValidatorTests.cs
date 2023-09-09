using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ForgotPasswordDtoValidatorTests
    {
        private ForgotPasswordDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ForgotPasswordDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Invalid_UserName_Is_Supplied([Values(null, "")] string userName)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(query => query.UsernameOrEmail, userName);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(query => query.UsernameOrEmail, UserTestSeedData.Performer.UserName);
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
