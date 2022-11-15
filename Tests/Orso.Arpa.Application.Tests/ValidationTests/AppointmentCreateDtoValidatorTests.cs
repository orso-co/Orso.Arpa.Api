using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentCreateDtoValidatorTests
    {
        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentCreateDtoValidator();
        }

        private AppointmentCreateDtoValidator _validator;

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_CategoryId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.CategoryId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_CategoryId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.CategoryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Status_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Status, AppointmentStatus.Refused);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_SalaryId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.SalaryId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SalaryId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.SalaryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_StartTime_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.StartTime, DateTime.MinValue);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StartTime_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.StartTime, FakeDateTime.UtcNow);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_EndTime_Is_Supplied()
        {
            _ = _validator.ShouldHaveValidationErrorForExact(command => command.EndTime, DateTime.MinValue);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_EndTime_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.EndTime, FakeDateTime.UtcNow);
        }

        [Test]
        public void Should_Have_Validation_Error_If_EndTime_Is_Not_Greater_Than_StartTime()
        {
            _ = _validator.ShouldHaveValidationErrorForExact(command => command.EndTime, new AppointmentCreateDto
            {
                StartTime = FakeDateTime.UtcNow,
                EndTime = FakeDateTime.UtcNow.AddHours(-3)
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_EndTime_Is_Greater_Than_StartTime()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.EndTime, new AppointmentCreateDto
            {
                StartTime = FakeDateTime.UtcNow,
                EndTime = FakeDateTime.UtcNow.AddHours(3)
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_EndTime_Is_Equal_To_StartTime()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.EndTime, new AppointmentCreateDto
            {
                StartTime = new DateTime(2019, 12, 28),
                EndTime = new DateTime(2019, 12, 28)
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            _ = _validator.ShouldHaveValidationErrorForExact(command => command.Name, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Name, "appointment");
        }

        [Test]
        public void Should_Fail_If_Invalid_Character_Is_Supplied([Values("<", ">", "}", "{", "[", "]", "\\", "=")] string name)
        {
            _ = _validator.ShouldHaveValidationErrorForExact(dto => dto.Name, name)
                .WithErrorMessage("Invalid character supplied. Please use only alphanumeric and whitespace characters " +
                "or one of the following: '-./(),$€#*%&„“\":;?!@+^°| +~_");
        }
    }
}
