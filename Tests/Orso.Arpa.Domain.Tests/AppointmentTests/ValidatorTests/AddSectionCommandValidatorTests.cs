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
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            _validAppointmentId = AppointmentSeedData.AfterShowParty.Id;
            _validSectionId = SectionSeedData.DeepFemaleVoices.Id;
            _mockAppointments = MockDbSets.Appointments;
            _mockSectionAppointments = MockDbSets.SectionAppointments;
            _mockSections = MockDbSets.Sections;
            _arpaContext.Appointments.Returns(_mockAppointments);
            _arpaContext.SectionAppointments.Returns(_mockSectionAppointments);
            _arpaContext.Sections.Returns(_mockSections);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command(Guid.NewGuid(), _validSectionId));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validSectionId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_SectionId_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command(_validAppointmentId, Guid.NewGuid()));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Section_Is_Already_Linked()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.SectionId, new Command(_validAppointmentId, SectionSeedData.Alto.Id));
        }
    }
}
