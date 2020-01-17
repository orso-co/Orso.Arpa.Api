using System;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Validation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Sections.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AddSectionDtoValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private AddSectionDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new AddSectionDtoValidator(
                _subReadOnlyRepository);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(default(Appointment));

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.AddSectionDto { Id = Guid.NewGuid(), SectionId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_SectionId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.SectionId, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_SectionId_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(default(Section));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.AddSectionDto { Id = Guid.NewGuid(), SectionId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_SectionId_Is_Supplied()
        {
            Section section = SectionSeedData.Alto;
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(section);

            _validator.ShouldNotHaveValidationErrorFor(command => command.SectionId, section.Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Section_Is_Already_Linked()
        {
            Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            Section section = SectionSeedData.Alto;
            appointment.SectionAppointments.Add(new SectionAppointment(section.Id, appointment.Id));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(appointment);
            _subReadOnlyRepository.GetByIdAsync<Section>(Arg.Any<Guid>()).Returns(section);

            _validator.ShouldHaveValidationErrorFor(command => command.SectionId, section.Id);
        }
    }
}
