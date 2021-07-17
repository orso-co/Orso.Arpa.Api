using System;
using FluentValidation.TestHelper;
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
            _bodyValidator.ShouldHaveValidationErrorForExact(dto => dto.DeactivationStart, DateTime.MinValue)
                .WithErrorMessage("'Deactivation Start' darf nicht leer sein.");
        }

        [Test]
        public void Should_Fail_If_Purpose_Exceeds_Max_Length()
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(dto => dto.Purpose, new string('#', 501))
                .WithErrorMessage("Die Länge von 'Purpose' muss kleiner oder gleich 500 sein. Sie haben 501 Zeichen eingegeben.");
        }

        [Test]
        public void Should_Fail_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(dto => dto.Id, Guid.Empty)
                .WithErrorMessage("'Id' darf nicht leer sein.");
        }

        [Test]
        public void Should_Succeed_If_Valid_DeactivationStart_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(dto => dto.DeactivationStart, DateTime.MaxValue);
        }

        [Test]
        public void Should_Succeed_If_Valid_Purpose_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(dto => dto.Purpose, new string('#', 500));
        }

        [Test]
        public void Should_Succeed_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(dto => dto.Id, Guid.NewGuid());
        }
    }
}
