using System;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.SelectValueMappings.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetResult;

namespace Orso.Arpa.Domain.Tests.AppointmentPerticipationsTests.ValidatorTests
{
    [TestFixture]
    public class SetParticipationResultCommandValidatorTests
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

            Func<ValidationResult> func = () => _validator.Validate(new Command
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                ResultId = Guid.NewGuid()
            });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);
            _subReadOnlyRepository.GetByIdAsync<Person>(Arg.Any<Guid>()).Returns(PersonSeedData.Orsoadmin);
            _subReadOnlyRepository.GetByIdAsync<SelectValueMapping>(Arg.Any<Guid>()).Returns(SelectValueMappingSeedData.AppointmentParticipationResultMappings[0]);
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_PersonId_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Person>(Arg.Any<Guid>()).Returns(default(Person));

            Func<ValidationResult> func = () => _validator.Validate(new Command
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                ResultId = Guid.NewGuid()
            });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_PersonId_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Person>(Arg.Any<Guid>()).Returns(PersonSeedData.Orsoadmin);
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);
            _subReadOnlyRepository.GetByIdAsync<SelectValueMapping>(Arg.Any<Guid>()).Returns(SelectValueMappingSeedData.AppointmentParticipationResultMappings[0]);
            _validator.ShouldNotHaveValidationErrorFor(command => command.PersonId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Result_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<SelectValueMapping>(Arg.Any<Guid>()).Returns(default(SelectValueMapping));

            Func<ValidationResult> func = () => _validator.Validate(new Command
            {
                Id = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                ResultId = Guid.NewGuid()
            });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ResultId_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<SelectValueMapping>(Arg.Any<Guid>()).Returns(SelectValueMappingSeedData.AppointmentParticipationResultMappings[0]);
            _subReadOnlyRepository.GetByIdAsync<Person>(Arg.Any<Guid>()).Returns(PersonSeedData.Orsoadmin);
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);
            _validator.ShouldNotHaveValidationErrorFor(command => command.ResultId, Guid.NewGuid());
        }
    }
}
