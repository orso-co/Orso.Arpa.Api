using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.ProjectApplication;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetProjectParticipationDtoValidatorTests
    {
        private SetProjectParticipationDtoValidator _validator;
        private SetProjectParticipationBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SetProjectParticipationDtoValidator();
            _bodyValidator = new SetProjectParticipationBodyDtoValidator();
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
        public void Should_Have_Validation_Error_If_Empty_MusicianProfileId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.MusicianProfileId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_MusicianProfileId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.MusicianProfileId, Guid.NewGuid());
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ParticipationStatusInternalId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.ParticipationStatusInternalId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_ParticipationStatusInternalId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.ParticipationStatusInternalId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_InvitationStatusId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.InvitationStatusId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_InvitationStatusId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.InvitationStatusId, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_CommentByStaffInner_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.CommentByStaffInner, new string('#', 501));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Comment_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.CommentByStaffInner, new string('#', 500));
        }

        [Test]
        public void Should_Have_Validation_Error_If_CommentTeam_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorFor(command => command.CommentTeam, new string('#', 501));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_CommentTeam_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorFor(command => command.CommentTeam, new string('#', 500));
        }

    }
}
