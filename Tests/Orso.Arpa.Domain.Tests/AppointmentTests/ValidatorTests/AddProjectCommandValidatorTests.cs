using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.AddProject;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class AddProjectCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private Guid _validAppointmentId;
        private Guid _validProjectId;
        private DbSet<Project> _mockProjects;
        private DbSet<Appointment> _mockAppointments;
        private DbSet<ProjectAppointment> _mockProjectAppointments;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);

            _mockProjects = MockDbSets.Projects;
            _arpaContext.Set<Project>().Returns(_mockProjects);

            _mockAppointments = MockDbSets.Appointments;
            _arpaContext.Set<Appointment>().Returns(_mockAppointments);

            _mockProjectAppointments = MockDbSets.ProjectAppointments;
            _arpaContext.Set<ProjectAppointment>().Returns(_mockProjectAppointments);
            _arpaContext.ProjectAppointments.Returns(_mockProjectAppointments);

            _validAppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id;
            _validProjectId = ProjectSeedData.HoorayForHollywood.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, _validProjectId));
        }

        [Test]
        public void Should_Have_Validation_Error_If_ProjectId_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.ProjectId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Project_Is_Already_Linked()
        {
            Guid linkedProjectId = ProjectSeedData.RockingXMas.Id;

            _validator.ShouldHaveValidationErrorFor(command => command.ProjectId, new Command(_validAppointmentId, linkedProjectId));
        }
    }
}
