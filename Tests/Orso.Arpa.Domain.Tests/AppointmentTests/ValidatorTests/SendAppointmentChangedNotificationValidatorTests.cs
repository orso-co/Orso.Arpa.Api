using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class SendAppointmentChangedNotificationValidatorTests
    {
        private IArpaContext _arpaContext;
        private SendAppointmentChangedNotification.Validator _validator;
        
        private DbSet<Appointment> _mockAppointmentDbSet;
        
        [SetUp]
        public void SetUp() {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new SendAppointmentChangedNotification.Validator(_arpaContext);
            _mockAppointmentDbSet = MockDbSets.Appointments;
            _arpaContext.Appointments.Returns(_mockAppointmentDbSet);
            _arpaContext.Set<Appointment>().Returns(_mockAppointmentDbSet);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Appointment_Does_Not_Exist() {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.AppointmentId, Guid.NewGuid(), nameof(Appointment));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Appointment_Has_No_Sections_Or_Projects() {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            Appointment appointment = AppointmentSeedData.RockingXMasConcert;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            var command = new SendAppointmentChangedNotification.Command {
                AppointmentId = appointment.Id,
                ForceSending = true
            };
            await _validator
                .ShouldHaveValidationErrorForExactAsync(c => c.AppointmentId, command);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Appointment_Has_No_Projects_And_Force_Sending_Is_False() {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            Appointment appointment = AppointmentSeedData.PhotoSession;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            await _validator
                .ShouldHaveValidationErrorForExactAsync(c => c.AppointmentId, appointment.Id)
            ;
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Appointment_Has_No_Projects_And_Force_Sending_Is_True() {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            Appointment appointment = AppointmentSeedData.PhotoSession;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            var command = new SendAppointmentChangedNotification.Command {
                AppointmentId = appointment.Id,
                ForceSending = true
            };
            await _validator
                .ShouldNotHaveValidationErrorForExactAsync(c => c.AppointmentId, command);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Appointment_Has_Projects_And_Sections() {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            Appointment appointment = AppointmentSeedData.AfterShowParty;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            await _validator
                .ShouldNotHaveValidationErrorForExactAsync(c => c.AppointmentId, appointment.Id)
            ;
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Appointment_Has_Projects() {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            _arpaContext.FindAsync<Appointment>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(appointment);
            await _validator
                .ShouldNotHaveValidationErrorForExactAsync(c => c.AppointmentId, appointment.Id)
            ;
        }
    }
}