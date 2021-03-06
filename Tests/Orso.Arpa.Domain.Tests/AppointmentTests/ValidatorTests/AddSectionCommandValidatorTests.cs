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
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.AddSection;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AddSectionCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private Guid _validAppointmentId;
        private Guid _validSectionId;
        private DbSet<Appointment> _mockAppointments;
        private DbSet<Section> _mockSections;
        private DbSet<SectionAppointment> _mockSectionAppointments;

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
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validSectionId = SectionSeedData.DeepFemaleVoices.Id;
            _mockAppointments = MockDbSets.Appointments;
            _mockSectionAppointments = MockDbSets.SectionAppointments;
            _mockSections = MockDbSets.Sections;
            _arpaContext.Set<Appointment>().Returns(_mockAppointments);
            _arpaContext.SectionAppointments.Returns(_mockSectionAppointments);
            _arpaContext.Set<SectionAppointment>().Returns(_mockSectionAppointments);
            _arpaContext.Set<Section>().Returns(_mockSections);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validSectionId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_SectionId_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.SectionId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Section_Is_Already_Linked()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.SectionId, new Command(_validAppointmentId, SectionSeedData.Alto.Id));
        }
    }
}
