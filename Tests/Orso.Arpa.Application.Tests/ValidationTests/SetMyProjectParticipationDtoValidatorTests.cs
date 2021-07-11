using System;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class SetMyProjectParticipationDtoValidatorTests
    {
        private SetMyProjectParticipationDtoValidator _validator;
        private SetMyProjectParticipationBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SetMyProjectParticipationDtoValidator();
            _bodyValidator = new SetMyProjectParticipationBodyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_ProjectId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.ProjectId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_CategoryId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.ProjectId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_StatusId_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.StatusId, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_StatusId_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.StatusId, Guid.NewGuid());
        }

        [Test]
        public void Should_Have_Validation_Error_If_Comment_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Comment, new string('#', 501));
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Comment_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(command => command.Comment, new string('#', 500));
        }
    }
}
