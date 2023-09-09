using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Enums;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class AppointmentParticipationSetResultDtoValidatorTests
    {
        private AppointmentParticipationSetResultDtoValidator _validator;
        private AppointmentParticipationSetResultBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new AppointmentParticipationSetResultDtoValidator();
            _bodyValidator = new();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Id, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_PersonId_Is_Supplied()
        {
            _ = await _validator.ShouldHaveValidationErrorForExactAsync(command => command.PersonId, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_PersonId_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.PersonId, Guid.NewGuid());
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Result_Is_Supplied()
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.Result, AppointmentParticipationResult.Absent);
        }
    }
}
