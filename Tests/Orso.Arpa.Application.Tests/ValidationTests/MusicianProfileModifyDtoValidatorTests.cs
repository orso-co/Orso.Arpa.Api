using System;
using System.Collections.Generic;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MusicianProfileModifyDtoValidatorTests
    {
        private MusicianProfileModifyDtoValidator _validator;
        private MusicianProfileModifyBodyDtoValidator _bodyValidator;

        [SetUp]
        public void Setup()
        {
            _validator = new MusicianProfileModifyDtoValidator();
            _bodyValidator = new MusicianProfileModifyBodyDtoValidator();
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
        public void Should_Have_Validation_Error_If_LevelAssessmentTeam_Is_Out_Of_Range()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.LevelAssessmentTeam, (byte)6);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_LevelAssessmentTeam_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.LevelAssessmentTeam, x);
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
        public void Should_Have_Validation_Error_If_ProfilePreferenceTeam_Is_Out_Of_Range()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.ProfilePreferenceTeam, (byte)6);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_ProfilePreferenceTeam_Is_In_Range([Values(0, 1, 5)] byte x)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.ProfilePreferenceTeam, x);
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
        public void Should_Succeed_If_Valid_BackgroundTeam_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(c => c.BackgroundTeam, new string('#', 1000));
        }

        [Test]
        public void Should_Have_Validation_Error_If_BackgroundTeam_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(c => c.BackgroundTeam, new string('#', 1001));
        }

        [Test]
        public void Should_Succeed_If_Valid_SalaryComment_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(c => c.SalaryComment, new string('#', 500));
        }

        [Test]
        public void Should_Have_Validation_Error_If_SalaryComment_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(c => c.SalaryComment, new string('#', 501));
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_QualificationId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.QualificationId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_QualificationId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(command => command.QualificationId, Guid.NewGuid());
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

        [Test]
        public void Should_Have_Validation_Error_If_PreferredPositionsTeam_Contains_Empty_Guid()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.PreferredPositionsTeamIds, new List<Guid>() { Guid.Empty });
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_PreferredPositionsTeam_Contains_Valid_Guid()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.PreferredPositionsTeamIds, new List<Guid>() { Guid.NewGuid() });
        }
    }
}
