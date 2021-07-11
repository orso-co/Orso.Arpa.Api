using System;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Tests.Shared.Extensions;

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
            _validator.ShouldHaveValidationErrorForExact(command => command.Title, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Title_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Title, "Valid title");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Title_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Title,
                "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        }
        public void Should_Have_Validation_Error_If_Empty_ShortTitle_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.ShortTitle, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ShortTitle_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.ShortTitle, "Valid short title");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Too_Long_ShortTitle_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.ShortTitle,
                "1234567890123456789012345678901"); // valid length exceeded
        }
        [Test]
        public void Should_Have_Validation_Error_If_Empty_Code_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Code, name);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Too_Long_Code_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Code,
                "1234567890123456789012345678901"); // valid length exceeded
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Code_Is_Supplied([Values("ABC1 -/0", "abcdefghijklmno", "pqrstuvwxyzABCD", "EFGHIJKLMNOPQRS", "TUVWXYZ01234567", "89/-?:().,+ ")] string code)
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Code, code);
        }

        [Test]

        // valid SEPA characters in DFÜ Abkommen(Deutsche Kreditwirtschaft)
        // a - z, A - Z, 0 - 9
        // special characters: / ? : ( ) . , ' + -
        // space character
        public void Should_Have_Validation_Error_If_Invalid_Character_In_Code_Is_Supplied([Values("ABC*", "ABC_", "ABCö", @"ABC\", "ABC{", "ABC[")] string code)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Code, code);
        }

        [Test]
        public void Should_Have_Validation_Error_If_EndDate_Is_Before_StartDate_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.EndDate, new ProjectCreateDto
            {
                StartDate = new DateTime(2020, 01, 01),
                EndDate = new DateTime(2020, 01, 01) - new TimeSpan(5, 0, 0, 0),
            });
        }
    }
}
