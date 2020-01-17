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
    public class SetVenueDtoValidatorTests
    {
        private IReadOnlyRepository _subReadOnlyRepository;
        private SetVenueDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _subReadOnlyRepository = Substitute.For<IReadOnlyRepository>();
            _validator = new SetVenueDtoValidator(
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

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.SetVenueDto { Id = Guid.NewGuid(), VenueId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Appointment>(Arg.Any<Guid>()).Returns(AppointmentSeedData.RockingXMasRehearsal);

            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_VenueId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.VenueId, default(Guid?));
        }

        [Test]
        public void Should_Have_Validation_Error_If_VenueId_Does_Not_Exist()
        {
            _subReadOnlyRepository.GetByIdAsync<Venue>(Arg.Any<Guid>()).Returns(default(Venue));

            Func<ValidationResult> func = () => _validator.Validate(new Dtos.SetVenueDto { Id = Guid.NewGuid(), VenueId = Guid.NewGuid() });

            func.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_VenueId_Is_Supplied()
        {
            _subReadOnlyRepository.GetByIdAsync<Venue>(Arg.Any<Guid>()).Returns(VenueSeedData.WeiherhofSchule);

            _validator.ShouldNotHaveValidationErrorFor(command => command.VenueId, Guid.NewGuid());
        }
    }
}
