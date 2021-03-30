using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ProjectModifyDtoValidatorTests
    {
        private ProjectModifyDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new ProjectModifyDtoValidator();
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

        public void Should_Have_Validation_Error_If_Too_Long_ShortTitle_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ShortTitle,
                "1234567890123456789012345678901");
        }
        public void Should_Have_Validation_Error_If_Empty_Number_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number, name);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Too_Long_Number_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number,
                "1234567890123456789012345678901"); 
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Number_Is_Supplied([Values("ABC1 -/0", "abcdefghijklmno", "pqrstuvwxyzABCD", "EFGHIJKLMNOPQRS", "TUVWXYZ01234567", "89/-?:().,+ ")] string number)
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Number, number);
        }

        [Test]

        public void Should_Have_Validation_Error_If_Invalid_Character_In_Number_Is_Supplied([Values("ABC*", "ABC_", "ABCö", @"ABC\", "ABC{", "ABC[")] string number)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Number, number);
        }

        [Test]
        public void Should_Have_Validation_Error_If_EndDate_Is_Before_StartDate_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.EndDate, new ProjectModifyDto
            {
                StartDate = new DateTime(2020, 01, 01),
                EndDate = new DateTime(2020, 01, 01) - new TimeSpan(5, 0, 0, 0),
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ParentId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ParentId, new ProjectModifyDto
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                ParentId = ProjectSeedData.HoorayForHollywood.Id
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ParentId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new ProjectModifyDto
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                ParentId = ProjectSeedData.RockingXMas.Id
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_No_ParentId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new ProjectModifyDto
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                ParentId = null
            });
        }
    }
}
