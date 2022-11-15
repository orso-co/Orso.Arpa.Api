using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetMyProjectAppointmentPredictionDtoValidatorTests
    {
        private SetMyProjectAppointmentPredictionDtoValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SetMyProjectAppointmentPredictionDtoValidator();
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
    }
}
