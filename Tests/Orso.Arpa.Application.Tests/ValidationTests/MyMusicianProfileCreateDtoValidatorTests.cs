using System;
using System.Collections.Generic;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MyMusicianProfileCreateDtoValidatorTests
    {
        private MyMusicianProfileCreateDtoValidator _bodyValidator;
        private MyDoublingInstrumentCreateBodyDtoValidator _doublingInstrumentValidator;

        [SetUp]
        public void Setup()
        {
            _bodyValidator = new MyMusicianProfileCreateDtoValidator();
            _doublingInstrumentValidator = new MyDoublingInstrumentCreateBodyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_LevelAssessmentPerformer_Is_Out_Of_Range([Values(0, 6)] byte x)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_LevelAssessmentPerformer_Is_In_Range([Values(1, 5)] byte x)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.InstrumentId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.InstrumentId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Comment_Exceeds_Max_Length()
        {
            _doublingInstrumentValidator.ShouldHaveValidationErrorForExact(command => command.Comment, new string('#', 501));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Comment_Is_Supplied()
        {
            _doublingInstrumentValidator.ShouldNotHaveValidationErrorForExact(command => command.Comment, new string('#', 500));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_DoublingInstrumentId_Is_Supplied()
        {
            _doublingInstrumentValidator.ShouldHaveValidationErrorForExact(command => command.InstrumentId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied()
        {
            _doublingInstrumentValidator.ShouldNotHaveValidationErrorForExact(command => command.InstrumentId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_DoublingInstrument_LevelAssessmentPerformer_Is_Out_Of_Range()
        {
            _doublingInstrumentValidator.ShouldHaveValidationErrorForExact(command => command.LevelAssessmentInner, (byte)6);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_DoublingInstrument_LevelAssessmentPerformer_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            _doublingInstrumentValidator.ShouldNotHaveValidationErrorForExact(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public void Should_Have_Validation_Error_If_PreferredPositionsInner_Contains_Empty_Guid()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(dto => dto.PreferredPositionsInnerIds, new List<Guid>() { Guid.Empty });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_PreferredPositionsInner_Contains_Valid_Guid()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(dto => dto.PreferredPositionsInnerIds, new List<Guid>() { Guid.NewGuid() });
        }
    }
}
