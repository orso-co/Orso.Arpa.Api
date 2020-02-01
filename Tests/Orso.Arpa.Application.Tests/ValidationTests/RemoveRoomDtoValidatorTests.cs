using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Application.Logic.Appointments.RemoveRoom;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RemoveRoomDtoValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new Validator();
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
        public void Should_Have_Validation_Error_If_Empty_RoomId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.RoomId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_RoomId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoomId, Guid.NewGuid());
        }
    }
}
