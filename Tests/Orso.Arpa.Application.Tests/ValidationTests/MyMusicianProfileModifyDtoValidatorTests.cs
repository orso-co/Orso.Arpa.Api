using System;
using System.Collections.Generic;
using NUnit.Framework;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MyMusicianProfileModifyDtoValidatorTests
    {
        private MyMusicianProfileModifyDtoValidator _validator;
        private MyMusicianProfileModifyBodyDtoValidator _bodyValidator;

        [SetUp]
        public void Setup()
        {
            _validator = new MyMusicianProfileModifyDtoValidator();
            _bodyValidator = new MyMusicianProfileModifyBodyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_LevelAssessmentInner_Is_Out_Of_Range()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.LevelAssessmentInner, (byte)6);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_LevelAssessmentInner_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.LevelAssessmentInner, x);
        }

        [Test]
        public void Should_Have_Validation_Error_If_ProfilePreferenceInner_Is_Out_Of_Range()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.ProfilePreferenceInner, (byte)6);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_ProfilePreferenceInner_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.ProfilePreferenceInner, x);
        }

        [Test]
        public void Should_Succeed_If_Valid_BackgroundInner_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(c => c.BackgroundInner, new string('#', 1000));
        }

        [Test]
        public void Should_Have_Validation_Error_If_BackgroundInner_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(c => c.BackgroundInner, new string('#', 1001));
        }

        [Test]
        public void Should_Have_Validation_Error_If_PreferredPositionsInner_Contains_Empty_Guid()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.PreferredPositionsInnerIds, new List<Guid>() { Guid.Empty });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_PreferredPositionsInner_Contains_Valid_Guid()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.PreferredPositionsInnerIds, new List<Guid>() { Guid.NewGuid() });
        }
    }
}
