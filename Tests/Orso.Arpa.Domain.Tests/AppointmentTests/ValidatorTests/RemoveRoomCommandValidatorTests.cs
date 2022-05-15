using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.RemoveRoom;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class RemoveRoomCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<AppointmentRoom> _mockAppointmentRooms;
        private Guid _validAppointmentId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mockAppointmentRooms = MockDbSets.AppointmentRooms;
            _arpaContext.AppointmentRooms.Returns(_mockAppointmentRooms);
            _validator = new Validator(_arpaContext);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new Command(_validAppointmentId, RoomSeedData.AulaWeiherhofSchule.Id));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Valid_RoomId_Is_Not_Linked()
        {
            Room room = RoomSeedData.MusikraumWeiherhofSchule;

            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.RoomId, new Command(_validAppointmentId, room.Id));
        }
    }
}
