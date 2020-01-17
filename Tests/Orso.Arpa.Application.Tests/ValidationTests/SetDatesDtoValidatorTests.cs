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
    public class SetDatesDtoValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private SetDatesDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new SetDatesDtoValidator(
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

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.SetDatesDto { Id = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }
    }
}
