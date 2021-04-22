using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentModifyDtoValidatorTests
    {
        private AppointmentModifyDtoValidator _validator;
        private AppointmentModifyBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentModifyDtoValidator();
            _bodyValidator = new AppointmentModifyBodyDtoValidator();
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
        public void Should_Not_Have_Validation_Error_If_Empty_CategoryId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.CategoryId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_CategoryId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.CategoryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_StatusId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.StatusId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StatusId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.StatusId, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_SalaryId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.SalaryId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SalaryId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.SalaryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_StartTime_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.StartTime, DateTime.MinValue);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StartTime_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.StartTime, FakeDateTime.UtcNow);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_EndTime_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.EndTime, DateTime.MinValue);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_EndTime_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.EndTime, FakeDateTime.UtcNow);
        }

        [Test]
        public void Should_Have_Validation_Error_If_EndTime_Is_Not_Greater_Than_StartTime()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Body.EndTime, new AppointmentModifyDto
            {
                Body = new AppointmentModifyBodyDto
                {
                    StartTime = FakeDateTime.UtcNow,
                    EndTime = FakeDateTime.UtcNow.AddHours(-3)
                }
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_EndTime_Is_Greater_Than_StartTime()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Body.EndTime, new AppointmentModifyDto
            {
                Body = new AppointmentModifyBodyDto
                {
                    StartTime = FakeDateTime.UtcNow,
                    EndTime = FakeDateTime.UtcNow.AddHours(3)
                }
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_EndTime_Is_Equal_To_StartTime()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Body.EndTime, new AppointmentModifyDto
            {
                Body = new AppointmentModifyBodyDto
                {
                    StartTime = new DateTime(2019, 12, 28),
                    EndTime = new DateTime(2019, 12, 28)
                }
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.Name, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.Name, "appointment");
        }
    }
}