using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Application.Logic.Appointments.Create;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentCreateDtoValidatorTests
    {
        [SetUp]
        public void SetUp()
        {
            _validator = new Validator();
        }

        private Validator _validator;

        [Test]
        public void Should_Have_Validation_Error_If_Empty_CategoryId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.CategoryId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_CategoryId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.CategoryId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_StatusId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.StatusId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StatusId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.StatusId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_EmolumentId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.EmolumentId, default(Guid?));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_EmolumentId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.EmolumentId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_StartTime_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.StartTime, DateTime.MinValue);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StartTime_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.StartTime, DateTime.UtcNow);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_EndTime_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.EndTime, DateTime.MinValue);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_EndTime_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.EndTime, DateTime.UtcNow);
        }

        [Test]
        public void Should_Have_Validation_Error_If_EndTime_Is_Not_Greater_Than_StartTime()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.EndTime, new Dto
            {
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(-3)
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_EndTime_Is_Greater_Than_StartTime()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.EndTime, new Dto
            {
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(3)
            });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_EndTime_Is_Equal_To_StartTime()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.EndTime, new Dto
            {
                StartTime = new DateTime(2019, 12, 28),
                EndTime = new DateTime(2019, 12, 28)
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")]string name)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Name, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Name, "appointment");
        }
    }
}
