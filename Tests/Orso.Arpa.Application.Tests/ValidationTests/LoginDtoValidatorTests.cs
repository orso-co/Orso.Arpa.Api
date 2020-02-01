using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Application.Logic.Auth.Login;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class LoginDtoValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Validator();
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
        public void Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied([Values(null, "")] string password)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.Password, password);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.Password, UserSeedData.ValidPassword);
        }
    }
}
