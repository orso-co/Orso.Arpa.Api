using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Application.Logic.Auth.SetRole;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetRoleDtoValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Validator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.UserName, username);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_RoleName_Is_Supplied([Values(null, "")] string roleName)
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoleName, roleName);
        }
    }
}
