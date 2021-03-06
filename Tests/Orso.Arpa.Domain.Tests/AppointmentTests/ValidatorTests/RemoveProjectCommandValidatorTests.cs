using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Appointments.RemoveProject;

namespace Orso.Arpa.Domain.Tests.AppointmentTests.ValidatorTests
{
    [TestFixture]
    public class RemoveProjectCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private DbSet<ProjectAppointment> _mockProjectAppointments;
        private Guid _validAppointmentId;

        [SetUp]
        public void SetUp()
        {
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _mockProjectAppointments = MockDbSets.ProjectAppointments;
            _arpaContext.ProjectAppointments.Returns(_mockProjectAppointments);
            _validator = new Validator(_arpaContext, localizer);
            _validAppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id;
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command(_validAppointmentId, ProjectSeedData.RockingXMas.Id));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Valid_ProjectId_Is_Not_Linked()
        {
            Project project = ProjectSeedData.HoorayForHollywood;

            _validator.ShouldHaveValidationErrorFor(command => command.ProjectId, new Command(_validAppointmentId, project.Id));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ProjectId_Is_Supplied()
        {
            Project project = ProjectSeedData.RockingXMas;

            _validator.ShouldNotHaveValidationErrorFor(command => command.ProjectId, new Command(_validAppointmentId, project.Id));
        }
    }
}
