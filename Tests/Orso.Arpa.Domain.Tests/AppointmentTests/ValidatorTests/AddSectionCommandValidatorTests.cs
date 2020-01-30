using System;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.AddSection;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AddSectionCommandValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private Validator _validator;
        private Appointment _appointment;
        private Section _section;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new Validator(
                _subReadOnlyRepository);
            _appointment = AppointmentSeedData.RockingXMasRehearsal;
            _section = SectionSeedData.Alto;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(default(Appointment));

            Func<ValidationResult> func = () => _validator.Validate(new Command(Guid.NewGuid(), Guid.NewGuid()));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(_appointment);
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(_section);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_SectionId_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(default(Section));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(_appointment);

            Func<ValidationResult> func = () => _validator.Validate(new Command(Guid.NewGuid(), Guid.NewGuid()));

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SectionId_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(_appointment);
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(_section);

            _validator.ShouldNotHaveValidationErrorFor(command => command.SectionId, _section.Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Section_Is_Already_Linked()
        {
            _appointment.SectionAppointments.Add(new SectionAppointment(_section.Id, _appointment.Id));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(_appointment);
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(_section);

            _validator.ShouldHaveValidationErrorFor(command => command.SectionId, _section.Id);
        }
    }
}
