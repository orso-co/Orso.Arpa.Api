using System;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentParticipationApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentParticipationSetResultDtoValidatorTests
    {
        private AppointmentParticipationSetResultDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentParticipationSetResultDtoValidator();
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
        public void Should_Have_Validation_Error_If_Empty_PersonId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.PersonId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_PersonId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.PersonId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_ResultId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.ResultId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ResultId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.ResultId, Guid.NewGuid());
        }
    }
}
