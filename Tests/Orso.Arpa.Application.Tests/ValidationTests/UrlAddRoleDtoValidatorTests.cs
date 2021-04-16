using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Tests.Shared.DtoTestData;

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

        [Test]
        public void Should_Have_Validation_Error_If_AdminRole_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.RoleId, RoleDtoData.Admin.Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_NonAdminRole_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoleId, RoleDtoData.Staff.Id);
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoleId, RoleDtoData.Performer.Id);
        }
    }
}
