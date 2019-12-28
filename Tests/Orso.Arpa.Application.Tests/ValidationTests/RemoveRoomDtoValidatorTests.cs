using System;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Validation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RemoveRoomDtoValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private RemoveRoomDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new RemoveRoomDtoValidator(
                _subReadOnlyRepository);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(default(Appointment));

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.RemoveRoomDto { Id = Guid.NewGuid(), RoomId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_RoomId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.RoomId, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Valid_RoomId_Is_Not_Linked()
        {
            Room room = RoomSeedData.AulaWeiherhofSchule;
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);
            _subReadOnlyRepository.GetByIdAsync<Room>(Arg.Any<Guid>()).Returns(room);

            _validator.ShouldHaveValidationErrorFor(command => command.RoomId, room.Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_RoomId_Is_Supplied()
        {
            Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            Room room = RoomSeedData.AulaWeiherhofSchule;
            appointment.AppointmentRooms.Add(new AppointmentRoom(appointment.Id, room.Id));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(appointment);
            _subReadOnlyRepository.GetByIdAsync<Room>(Arg.Any<Guid>()).Returns(room);

            _validator.ShouldNotHaveValidationErrorFor(command => command.RoomId, room.Id);
        }
    }
}
