using System;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentSetVenueDtoValidatorTests
    {
        private AppointmentSetVenueDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentSetVenueDtoValidator();
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
        public void Should_Have_Validation_Error_If_Empty_VenueId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.VenueId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_VenueId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.VenueId, Guid.NewGuid());
        }
    }
}
