using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RegionModifyDtoValidatorTests
    {
        private RegionModifyDtoValidator _validator;
        private RegionModifyBodyDtoValidator _bodyValidator;

        [SetUp]
        public void Setup()
        {
            _validator = new RegionModifyDtoValidator();
            _bodyValidator = new RegionModifyBodyDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(dto => dto.Id, Guid.Empty);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.Name, name);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            await _bodyValidator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.Name, "Honolulu");
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(dto => dto.Id, Guid.NewGuid());
        }
    }
}
