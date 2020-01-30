using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.Validation;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetDatesDtoValidatorTests
    {
        private SetDatesDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SetDatesDtoValidator();
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
    }
}
