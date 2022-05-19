using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.SetDates;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class SetDatesCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private IMapper _mapper;
        private Validator _validator;
        private DbSet<Appointment> _mockAppointmentDbSet;
        private Appointment _existingAppointment;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mapper = Substitute.For<IMapper>();
            _validator = new Validator(_arpaContext, _mapper);
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
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new Command { Id = _existingAppointment.Id });
        }
    }
}
