using System;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MusicianProfileDeactivationCreateDtoValidatorTests
    {
        private readonly MusicianProfileDeactivationCreateDtoValidator _validator = new MusicianProfileDeactivationCreateDtoValidator();
        private readonly MusicianProfileDeactivationCreateBodyDtoValidator _bodyValidator = new MusicianProfileDeactivationCreateBodyDtoValidator();

        [Test]
        public void Should_Fail_If_Empty_DeactivationStart_Is_Supplied()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.DeactivationStart, DateTime.MinValue);
        }

        [Test]
        public void Should_Fail_If_Purpose_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.Purpose, new string('#', 501));
        }

        [Test]
        public void Should_Fail_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExactAsync(dto => dto.Id, Guid.Empty);
        }

        [Test]
        public void Should_Succeed_If_Valid_DeactivationStart_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.DeactivationStart, DateTime.MaxValue);
        }

        [Test]
        public void Should_Succeed_If_Valid_Purpose_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.Purpose, new string('#', 500));
        }

        [Test]
        public void Should_Succeed_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.Id, Guid.NewGuid());
        }
    }
}
