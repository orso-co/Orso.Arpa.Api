using System;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentAddSectionDtoValidatorTests
    {
        private AppointmentAddSectionDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentAddSectionDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_SectionId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.SectionId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SectionId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.SectionId, Guid.NewGuid());
        }
    }
}
