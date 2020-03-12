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
using static Orso.Arpa.Domain.Logic.Appointments.SetVenue;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class SetVenueCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<Appointment> _mockAppointmentDbSet;
        private DbSet<Venue> _mockVenueDbSet;
        private Guid _validAppointmentId;
        private Guid _validVenueId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockAppointmentDbSet = MockDbSets.Appointments;
            _arpaContext.Appointments.Returns(_mockAppointmentDbSet);
            _mockVenueDbSet = MockDbSets.Venues;
            _arpaContext.Venues.Returns(_mockVenueDbSet);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validVenueId = VenueSeedData.WeiherhofSchule.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command(Guid.NewGuid(), _validVenueId));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validVenueId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_VenueId_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command(_validAppointmentId, Guid.NewGuid()));

            func.Should().Throw<RestException>();
        }
    }
}
