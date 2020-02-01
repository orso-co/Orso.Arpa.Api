using System;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.RemoveProject;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class RemoveProjectCommandValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new Validator(
                _subReadOnlyRepository);
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
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Valid_ProjectId_Is_Not_Linked()
        {
            Project project = ProjectSeedData.RockingXMas;
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasConcert);
            _subReadOnlyRepository.GetByIdAsync<Project>(Arg.Any<Guid>()).Returns(project);

            _validator.ShouldHaveValidationErrorFor(command => command.ProjectId, project.Id);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ProjectId_Is_Supplied()
        {
            Appointment appointment = AppointmentSeedData.RockingXMasRehearsal;
            Project project = ProjectSeedData.RockingXMas;
            appointment.ProjectAppointments.Add(new ProjectAppointment(project.Id, appointment.Id));
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(appointment);
            _subReadOnlyRepository.GetByIdAsync<Project>(Arg.Any<Guid>()).Returns(project);

            _validator.ShouldNotHaveValidationErrorFor(command => command.ProjectId, project.Id);
        }
    }
}
