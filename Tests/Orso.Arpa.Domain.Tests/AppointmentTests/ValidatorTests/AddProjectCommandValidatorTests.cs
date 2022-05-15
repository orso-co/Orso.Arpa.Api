using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Tests.Shared.Extensions;
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
        private DbSet<ProjectAppointment> _mockProjectAppointments;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);

            _mockProjectAppointments = MockDbSets.ProjectAppointments;
            _arpaContext.ProjectAppointments.Returns(_mockProjectAppointments);

            _validAppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id;
            _validProjectId = ProjectSeedData.HoorayForHollywood.Id;
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.Id, Guid.NewGuid(), nameof(Appointment));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldNotHaveValidationErrorForExactAsync(c => c.Id, new Command(_validAppointmentId, _validProjectId));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_ProjectId_Does_Not_Exist()
        {
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.ProjectId, Guid.NewGuid(), nameof(Project));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Project_Is_Already_Linked()
        {
            Guid linkedProjectId = ProjectSeedData.RockingXMas.Id;
            _arpaContext.EntityExistsAsync<Project>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _arpaContext.EntityExistsAsync<Appointment>(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            await _validator.ShouldHaveValidationErrorForExactAsync(c => c.ProjectId, new Command(_validAppointmentId, linkedProjectId));
        }
    }
}
