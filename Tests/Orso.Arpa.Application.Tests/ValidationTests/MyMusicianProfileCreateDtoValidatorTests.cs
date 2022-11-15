using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
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
        public async Task Should_Have_Validation_Error_If_LevelAssessmentPerformer_Is_Out_Of_Range([Values(0, 6)] byte x)
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_LevelAssessmentPerformer_Is_In_Range([Values(1, 5)] byte x)
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.InstrumentId, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.InstrumentId, Guid.NewGuid());
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Comment_Exceeds_Max_Length()
        {
            await _doublingInstrumentValidator.ShouldHaveValidationErrorForExactAsync(command => command.Comment, new string('#', 501));
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Comment_Is_Supplied()
        {
            await _doublingInstrumentValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.Comment, new string('#', 500));
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_DoublingInstrumentId_Is_Supplied()
        {
            await _doublingInstrumentValidator.ShouldHaveValidationErrorForExactAsync(command => command.InstrumentId, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_DoublingInstrumentId_Is_Supplied()
        {
            await _doublingInstrumentValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.InstrumentId, Guid.NewGuid());
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_DoublingInstrument_LevelAssessmentPerformer_Is_Out_Of_Range()
        {
            await _doublingInstrumentValidator.ShouldHaveValidationErrorForExactAsync(command => command.LevelAssessmentInner, (byte)6);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_DoublingInstrument_LevelAssessmentPerformer_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            await _doublingInstrumentValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_PreferredPositionsInner_Contains_Empty_Guid()
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.PreferredPositionsInnerIds, new List<Guid>() { Guid.Empty });
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_PreferredPositionsInner_Contains_Valid_Guid()
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.PreferredPositionsInnerIds, new List<Guid>() { Guid.NewGuid() });
        }
    }
}
