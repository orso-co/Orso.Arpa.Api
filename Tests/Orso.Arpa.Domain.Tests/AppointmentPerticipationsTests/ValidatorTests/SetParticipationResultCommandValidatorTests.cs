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
            _arpaContext = Substitute.For<IArpaContext>();
            _validator = new Validator(_arpaContext);
            DbSet<Appointment> mockAppointments = MockDbSets.Appointments;
            _arpaContext.Set<Appointment>().Returns(mockAppointments);
            DbSet<Person> mockPersons = MockDbSets.Persons;
            _arpaContext.Set<Person>().Returns(mockPersons);
            DbSet<SelectValueMapping> mockMappings = MockDbSets.SelectValueMappints;
            _arpaContext.Set<SelectValueMapping>().Returns(mockMappings);
            _validResultId = SelectValueMappingSeedData.AppointmentParticipationResultMappings[0].Id;
            _validPersonId = PersonSeedData.Orsianer.Id;
            _validAppointmentId = AppointmentSeedData.RockingXMasRehearsal.Id;
        }

        [Test]
        public void Should_Have_Validation_Error_If_Id_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command
            {
                Id = Guid.NewGuid(),
                PersonId = _validPersonId,
                ResultId = _validResultId
            });

            func.Should().Throw<NotFoundException>();
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
            Func<ValidationResult> func = () => _validator.Validate(new Command
            {
                Id = _validAppointmentId,
                PersonId = Guid.NewGuid(),
                ResultId = _validResultId
            });

            func.Should().Throw<NotFoundException>();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Result_Does_Not_Exist()
        {
            Func<ValidationResult> func = () => _validator.Validate(new Command
            {
                Id = _validAppointmentId,
                PersonId = _validPersonId,
                ResultId = Guid.NewGuid()
            });

            func.Should().Throw<NotFoundException>();
        }
    }
}
