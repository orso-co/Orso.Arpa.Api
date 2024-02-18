using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class RemoveSectionCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private RemoveSectionFromAppointment.Validator _validator;
        private DbSet<SectionAppointment> _mockSectionAppointments;
        private Guid _validAppointmentId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new RemoveSectionFromAppointment.Validator(_arpaContext);
            _mockSectionAppointments = MockDbSets.SectionAppointments;
            _arpaContext.Set<SectionAppointment>().Returns(_mockSectionAppointments);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new RemoveSectionFromAppointment.Command(_validAppointmentId, SectionSeedData.Alto.Id));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Valid_SectionId_Is_Not_Linked()
        {
            Section section = SectionSeedData.HighFemaleVoices;

            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.SectionId, new RemoveSectionFromAppointment.Command(_validAppointmentId, section.Id));
        }
    }
}
