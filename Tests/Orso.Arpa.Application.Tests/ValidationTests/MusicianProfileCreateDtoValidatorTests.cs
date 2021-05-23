using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MusicianProfileCreateDtoValidatorTests
    {
        private MusicianProfileCreateDtoValidator _validator;
        private MusicianProfileCreateBodyDtoValidator _bodyValidator;

        [SetUp]
        public void Setup()
        {
            _validator = new MusicianProfileCreateDtoValidator();
            _bodyValidator = new MusicianProfileCreateBodyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_LevelAssessmentPerformer_Is_Out_Of_Range()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.LevelAssessmentPerformer, (byte)6);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_LevelAssessmentPerformer_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.LevelAssessmentPerformer, x);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_QualificationId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.QualificationId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.QualificationId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.InstrumentId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.InstrumentId, Guid.NewGuid());
        }

    }
}
