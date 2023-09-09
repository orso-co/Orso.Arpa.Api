using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ProjectModifyDtoValidatorTests
    {
        private ProjectModifyDtoValidator _validator;
        private ProjectModifyBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new ProjectModifyDtoValidator();
            _bodyValidator = new ProjectModifyBodyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Title_Is_Supplied([Values(null, "")] string name)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Title, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Title_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.Title, "Valid title");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Title_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Title,
                "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        }
        public void Should_Have_Validation_Error_If_Empty_ShortTitle_Is_Supplied([Values(null, "")] string name)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.ShortTitle, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ShortTitle_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.ShortTitle, "Valid short title");
        }

        public void Should_Have_Validation_Error_If_Too_Long_ShortTitle_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.ShortTitle,
                "1234567890123456789012345678901");
        }
        public void Should_Have_Validation_Error_If_Empty_Code_Is_Supplied([Values(null, "")] string name)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Code, name);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Too_Long_Code_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Code,
                "1234567890123456789012345678901");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Code_Is_Supplied([Values("ABC1 -/0", "abcdefghijklmno", "pqrstuvwxyzABCD", "EFGHIJKLMNOPQRS", "TUVWXYZ01234567", "89/-?:().,+ ")] string code)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.Code, code);
        }

        [Test]

        public void Should_Have_Validation_Error_If_Invalid_Character_In_Code_Is_Supplied([Values("ABC*", "ABC_", "ABCö", @"ABC\", "ABC{", "ABC[")] string code)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Code, code);
        }

        [Test]
        public void Should_Have_Validation_Error_If_EndDate_Is_Before_StartDate_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.EndDate, new ProjectModifyBodyDto
            {
                StartDate = new DateTime(2020, 01, 01),
                EndDate = new DateTime(2020, 01, 01) - new TimeSpan(5, 0, 0, 0),
            }).WithErrorMessage("EndDate must be greater than EndDate");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_ParentId_Is_Supplied()
        {
            var dto = new ProjectModifyDto
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                Body = new ProjectModifyBodyDto
                {
                    ParentId = ProjectSeedData.HoorayForHollywood.Id
                }
            };
            TestValidationResult<ProjectModifyDto> result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(dto => dto.Body.ParentId)
                .WithErrorMessage("The project must not be its own parent");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ParentId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Id, new ProjectModifyDto
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                Body = new ProjectModifyBodyDto
                {
                    ParentId = ProjectSeedData.RockingXMas.Id
                }
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_No_ParentId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Id, new ProjectModifyDto
            {
                Id = ProjectSeedData.HoorayForHollywood.Id,
                Body = new ProjectModifyBodyDto
                {
                    ParentId = null
                }
            });
        }
    }
}
