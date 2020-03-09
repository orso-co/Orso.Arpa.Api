using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using static Orso.Arpa.Application.AuthApplication.SetRoleDto;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetRoleDtoValidatorTests
    {
        private SetRoleDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new SetRoleDtoValidator();
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
