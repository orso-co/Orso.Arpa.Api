using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.Validation;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RemoveSectionDtoValidatorTests
    {
        private RemoveSectionDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new RemoveSectionDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_SectionId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.SectionId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SectionId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.SectionId, Guid.NewGuid());
        }
    }
}
