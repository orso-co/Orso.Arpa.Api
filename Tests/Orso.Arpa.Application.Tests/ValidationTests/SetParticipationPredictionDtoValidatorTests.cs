using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Application.Logic.Me.SetPrediction;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetParticipationPredictionDtoValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new Validator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_PredictionId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.PredictionId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_PredictionId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.PredictionId, Guid.NewGuid());
        }
    }
}
