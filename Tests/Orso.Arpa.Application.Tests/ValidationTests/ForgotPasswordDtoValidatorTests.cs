using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
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
        public void Should_Have_Validation_Error_If_Invalid_UserName_Is_Supplied([Values(null, "")] string userName)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.UserName, userName);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.UserName, UserSeedData.Orsianer.UserName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ClientUri_Is_Supplied([Values(null, "", "http:/mw1.google.com", "foo/bar")] string clientUri)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.ClientUri, clientUri);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ClientUri_Is_Supplied([Values("http://localhost:4200", "https://www.google.de")] string clientUri)
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.ClientUri, clientUri);
        }
    }
}
