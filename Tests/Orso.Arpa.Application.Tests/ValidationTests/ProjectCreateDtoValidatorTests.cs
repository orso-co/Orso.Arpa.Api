using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ProjectCreateDtoValidatorTests
    {
        private ProjectCreateDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new ProjectCreateDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Title_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Title, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Title_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Title, "Valid title");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Title_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Title,
                "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        }
        public void Should_Have_Validation_Error_If_Empty_ShortTitle_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ShortTitle, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ShortTitle_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.ShortTitle, "Valid short title");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ShortTitle_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ShortTitle,
                "1234567890123456789012345678901");
        }
        public void Should_Have_Validation_Error_If_Empty_Number_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Number_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Number, "ValidNum1");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Number_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number,
                "1234567890123456789012345678901");
        }
        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Character_In_Number_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number,
                "ABC1 -/0");    // space is the illegal character
        }
    }
}
