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
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AddProjectDtoValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private AddProjectDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new AddProjectDtoValidator(
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

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.AddProjectDto { Id = Guid.NewGuid(), ProjectId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_ProjectId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.ProjectId, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_ProjectId_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Project>(Arg.Any<Guid>()).Returns(default(Project));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.AddProjectDto { Id = Guid.NewGuid(), ProjectId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ProjectId_Is_Supplied()
        {
            Project project = ProjectSeedData.RockingXMas;
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);
            _subReadOnlyRepository.GetByIdAsync<Project>(Arg.Any<Guid>()).Returns(project);

            _validator.ShouldNotHaveValidationErrorFor(command => command.ProjectId, project.Id);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Project_Is_Already_Linked()
        {
            Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            Project project = ProjectSeedData.RockingXMas;
            appointment.ProjectAppointments.Add(new ProjectAppointment(project.Id, appointment.Id));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(appointment);
            _subReadOnlyRepository.GetByIdAsync<Project>(Arg.Any<Guid>()).Returns(project);

            _validator.ShouldHaveValidationErrorFor(command => command.ProjectId, project.Id);
        }
    }
}
