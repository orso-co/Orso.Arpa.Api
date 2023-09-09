using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UrlAddRoleDtoValidatorTests
    {
        private UrlAddRoleDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UrlAddRoleDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_RoleId_Is_Supplied()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.RoleId, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_RoleId_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.RoleId, Guid.NewGuid());
        }
    }
}
