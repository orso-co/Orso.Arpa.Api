using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Tests.Shared.Extensions;

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
        public async Task Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Username, username);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Empty_RoleName_Is_Supplied([Values(null, "")] string roleName)
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.RoleNames, new[] { roleName });
        }
    }
}
