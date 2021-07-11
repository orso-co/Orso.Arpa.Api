using System;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentRemoveRoomDtoValidatorTests
    {
        private AppointmentRemoveRoomDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentRemoveRoomDtoValidator();
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
        public void Should_Have_Validation_Error_If_Empty_RoomId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.RoomId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_RoomId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.RoomId, Guid.NewGuid());
        }
    }
}
