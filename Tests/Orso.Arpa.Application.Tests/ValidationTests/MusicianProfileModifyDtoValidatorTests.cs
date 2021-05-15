using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MusicianProfileModifyDtoValidatorTests
    {
        private MusicianProfileModifyDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MusicianProfileModifyDtoValidator();
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
        public void Should_Have_Validation_Error_If_LevelAssessmentPerformer_Is_Out_Of_Range([Values((byte)0, (byte)6)] byte x)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.LevelAssessmentPerformer, x);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_LevelAssessmentPerformer_Is_In_Range([Values(1, 3, 5)] byte x)
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.LevelAssessmentPerformer, x);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_QualificationId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.QualificationId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.QualificationId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_InstrumentId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.InstrumentId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InstrumentId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.InstrumentId, Guid.NewGuid());
        }
    }
}
