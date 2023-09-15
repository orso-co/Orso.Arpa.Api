using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class SetDatesCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private SetDates.Validator _validator;
        private DbSet<Appointment> _mockAppointmentDbSet;
        private Appointment _existingAppointment;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new SetDates.Validator(_arpaContext);
            _mockAppointmentDbSet = MockDbSets.Appointments;
            _existingAppointment = AppointmentSeedData.RockingXMasRehearsal;
            _mockAppointmentDbSet.FindAsync(Arg.Any<Guid>()).Returns(_existingAppointment);
            _arpaContext.Set<Appointment>().Returns(_mockAppointmentDbSet);
            _arpaContext.Appointments.Returns(_mockAppointmentDbSet);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.Id, Guid.NewGuid(), nameof(Appointment));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new SetDates.Command { Id = _existingAppointment.Id });
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_EndDate_Is_Not_After_Begin_Date()
        {
            _arpaContext.Appointments.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeAppointments.RockingXMasRehearsal);
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.EndTime, new DateTime(2019, 12, 20, 10, 00, 00));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_EndDate_Is_After_Begin_Date()
        {
            _arpaContext.Appointments.FindAsync(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(FakeAppointments.RockingXMasRehearsal);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.EndTime, new DateTime(2019, 12, 31, 10, 00, 00));
        }
    }
}
