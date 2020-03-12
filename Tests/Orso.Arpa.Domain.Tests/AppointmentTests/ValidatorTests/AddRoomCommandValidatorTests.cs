using System;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.AddRoom;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AddRoomCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<Appointment> _mockAppointments;
        private DbSet<AppointmentRoom> _mockAppointmentRooms;
        private DbSet<Room> _mockRooms;
        private Guid _validAppointmentId;
        private Guid _validRoomId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockAppointments = MockDbSets.Appointments;
            _mockAppointmentRooms = MockDbSets.AppointmentRooms;
            _mockRooms = MockDbSets.Rooms;
            _arpaContext.Appointments.Returns(_mockAppointments);
            _arpaContext.AppointmentRooms.Returns(_mockAppointmentRooms);
            _arpaContext.Rooms.Returns(_mockRooms);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validRoomId = RoomSeedData.MusikraumWeiherhofSchule.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command(Guid.NewGuid(), _validRoomId));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validRoomId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_RoomId_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command(_validAppointmentId, Guid.NewGuid()));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Room_Is_Already_Linked()
        {
            Guid linkedRoomId = RoomSeedData.AulaWeiherhofSchule.Id;

            _validator.ShouldHaveValidationErrorFor(command => command.RoomId, new Command(_validAppointmentId, linkedRoomId));
        }
    }
}
