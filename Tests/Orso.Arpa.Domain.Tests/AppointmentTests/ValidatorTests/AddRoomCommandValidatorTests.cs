using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AddRoomCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private AddRoom.Validator _validator;
        private DbSet<AppointmentRoom> _mockAppointmentRooms;
        private Guid _validAppointmentId;
        private Guid _validRoomId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new AddRoom.Validator(_arpaContext);
            _mockAppointmentRooms = MockDbSets.AppointmentRooms;
            _arpaContext.AppointmentRooms.Returns(_mockAppointmentRooms);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validRoomId = RoomSeedData.MusikraumWeiherhofSchule.Id;
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.Id, Guid.NewGuid(), nameof(Appointment));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Room>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new AddRoom.Command(_validAppointmentId, _validRoomId));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_RoomId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Room>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.RoomId, Guid.NewGuid(), nameof(Room));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Room_Is_Already_Linked()
        {
            Guid linkedRoomId = RoomSeedData.AulaWeiherhofSchule.Id;
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Room>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.RoomId, new AddRoom.Command(_validAppointmentId, linkedRoomId));
        }
    }
}
