using System;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.RemoveSection;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class RemoveSectionCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<SectionAppointment> _mockSectionAppointments;
        private Guid _validAppointmentId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _mockSectionAppointments = MockDbSets.SectionAppointments;
            _arpaContext.SectionAppointments.Returns(_mockSectionAppointments);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Id, new Command(_validAppointmentId, SectionSeedData.Alto.Id));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Valid_SectionId_Is_Not_Linked()
        {
            Section section = SectionSeedData.HighFemaleVoices;

            _validator.ShouldHaveValidationErrorForExact(command => command.SectionId, new Command(_validAppointmentId, section.Id));
        }
    }
}
