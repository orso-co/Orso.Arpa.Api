using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class RemoveProjectCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private RemoveProject.Validator _validator;
        private DbSet<ProjectAppointment> _mockProjectAppointments;
        private Guid _validAppointmentId;

        [SetUp]
        public void SetUp()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _mockProjectAppointments = MockDbSets.ProjectAppointments;
            _arpaContext.ProjectAppointments.Returns(_mockProjectAppointments);
            _validator = new RemoveProject.Validator(_arpaContext);
            _validAppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id;
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, new RemoveProject.Command(_validAppointmentId, ProjectSeedData.RockingXMas.Id));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Valid_ProjectId_Is_Not_Linked()
        {
            Project project = ProjectSeedData.HoorayForHollywood;

            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.ProjectId, new RemoveProject.Command(_validAppointmentId, project.Id));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ProjectId_Is_Supplied()
        {
            Project project = ProjectSeedData.RockingXMas;

            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.ProjectId, new RemoveProject.Command(_validAppointmentId, project.Id));
        }
    }
}
