using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.UrlApplication;

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
        public void Should_Have_Validation_Error_If_Empty_RoleId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.RoleId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_RoleId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoleId, Guid.NewGuid());
        }
    }
}
