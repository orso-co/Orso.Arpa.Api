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
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext, localizer);
            _mockAppointmentDbSet = MockDbSets.Appointments;
            _arpaContext.Set<Appointment>().Returns(_mockAppointmentDbSet);
            _mockVenueDbSet = MockDbSets.Venues;
            _arpaContext.Set<Venue>().Returns(_mockVenueDbSet);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validVenueId = VenueSeedData.WeiherhofSchule.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validVenueId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_VenueId_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.VenueId, Guid.NewGuid());
        }
    }
}
