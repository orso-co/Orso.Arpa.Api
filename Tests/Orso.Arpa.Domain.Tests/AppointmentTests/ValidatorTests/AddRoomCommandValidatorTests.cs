using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
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
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext, localizer);
            _mockAppointments = MockDbSets.Appointments;
            _mockAppointmentRooms = MockDbSets.AppointmentRooms;
            _mockRooms = MockDbSets.Rooms;
            _arpaContext.Set<Appointment>().Returns(_mockAppointments);
            _arpaContext.Set<AppointmentRoom>().Returns(_mockAppointmentRooms);
            _arpaContext.AppointmentRooms.Returns(_mockAppointmentRooms);
            _arpaContext.Set<Room>().Returns(_mockRooms);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validRoomId = RoomSeedData.MusikraumWeiherhofSchule.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validRoomId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_RoomId_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.RoomId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Room_Is_Already_Linked()
        {
            Guid linkedRoomId = RoomSeedData.AulaWeiherhofSchule.Id;

            _validator.ShouldHaveValidationErrorFor(command => command.RoomId, new Command(_validAppointmentId, linkedRoomId));
        }
    }
}
