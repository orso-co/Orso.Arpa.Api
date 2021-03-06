using System;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application;
using Orso.Arpa.Application.Resources.Cultures;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetResult;

namespace Orso.Arpa.Domain.Tests.AppointmentPerticipationsTests.ValidatorTests
{
    [TestFixture]
    public class SetParticipationResultCommandValidatorTests
    {
        private IArpaContext _arpaContext;
        private Validator _validator;
        private Guid _validAppointmentId;
        private Guid _validPersonId;
        private Guid _validResultId;

        [SetUp]
        public void SetUp()
        {
            IStringLocalizer<DomainResource>  localizer =
                new StringLocalizer<DomainResource> (
                    new ResourceManagerStringLocalizerFactory(
                        new OptionsWrapper<LocalizationOptions>(new LocalizationOptions()),
                        new LoggerFactory()));
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext, localizer);
            DbSet<Appointment> mockAppointments = MockDbSets.Appointments;
            _arpaContext.Set<Appointment>().Returns(mockAppointments);
            DbSet<Person> mockPersons = MockDbSets.Persons;
            _arpaContext.Set<Person>().Returns(mockPersons);
            DbSet<SelectValueMapping> mockMappings = MockDbSets.SelectValueMappints;
            _arpaContext.Set<SelectValueMapping>().Returns(mockMappings);
            _validResultId = SelectValueMappingSeedData.AppointmentParticipationResultMappings[0].Id;
            _validPersonId = PersonSeedData.Performer.Id;
            _validAppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Ids_Are_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, new Command()
            {
                Id = _validAppointmentId,
                PersonId = _validPersonId,
                ResultId = _validResultId
            });
        }

        [Test]
        public void Should_Have_Validation_Error_If_PersonId_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.PersonId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Result_Does_Not_Exist()
        {
            _validator.ShouldHaveValidationErrorFor(c => c.ResultId, Guid.NewGuid());
        }
    }
}
