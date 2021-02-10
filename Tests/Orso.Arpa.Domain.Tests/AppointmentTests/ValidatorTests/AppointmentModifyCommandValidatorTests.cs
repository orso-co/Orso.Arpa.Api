using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.Modify;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AppointmentModifyCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<Appointment> _mockAppointmentDbSet;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockAppointmentDbSet = MockDbSets.Appointments;
            _arpaContext.Set<Appointment>().Returns(_mockAppointmentDbSet);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, AppointmentSeedData.RockingXMasRehearsal.Id);
        }
    }
}
