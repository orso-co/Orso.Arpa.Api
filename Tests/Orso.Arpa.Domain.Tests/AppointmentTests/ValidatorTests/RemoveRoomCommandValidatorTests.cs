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
using Orso.Arpa.Domain.Resources.Cultures;
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
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _mockAppointmentRooms = MockDbSets.AppointmentRooms;
            _arpaContext.AppointmentRooms.Returns(_mockAppointmentRooms);
            _validator = new Validator(_arpaContext, localizer);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, RoomSeedData.AulaWeiherhofSchule.Id));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Valid_RoomId_Is_Not_Linked()
        {
            Room room = RoomSeedData.MusikraumWeiherhofSchule;

            _validator.ShouldHaveValidationErrorFor(command => command.RoomId, new Command(_validAppointmentId, room.Id));
        }
    }
}
