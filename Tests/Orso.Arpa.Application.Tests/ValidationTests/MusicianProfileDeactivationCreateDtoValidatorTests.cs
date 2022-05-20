using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MusicianProfileDeactivationCreateDtoValidatorTests
    {
        private readonly MusicianProfileDeactivationCreateDtoValidator _validator = new();
        private readonly MusicianProfileDeactivationCreateBodyDtoValidator _bodyValidator = new();

        [Test]
        public async Task Should_Fail_If_Empty_DeactivationStart_Is_Supplied()
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.DeactivationStart, DateTime.MinValue);
        }

        [Test]
        public async Task Should_Fail_If_Purpose_Exceeds_Max_Length()
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.Purpose, new string('#', 501));
        }

        [Test]
        public async Task Should_Fail_If_Empty_Id_Is_Supplied()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(dto => dto.Id, Guid.Empty);
        }

        [Test]
        public async Task Should_Succeed_If_Valid_DeactivationStart_Is_Supplied()
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.DeactivationStart, DateTime.MaxValue);
        }

        [Test]
        public async Task Should_Succeed_If_Valid_Purpose_Is_Supplied()
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.Purpose, new string('#', 500));
        }

        [Test]
        public async Task Should_Succeed_If_Valid_Id_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.Id, Guid.NewGuid());
        }
    }
}
