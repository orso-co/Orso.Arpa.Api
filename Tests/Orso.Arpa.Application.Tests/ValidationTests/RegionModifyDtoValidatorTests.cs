using System;
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
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(dto => dto.Id, Guid.Empty);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(dto => dto.Name, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(dto => dto.Name, "Honolulu");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(dto => dto.Id, Guid.NewGuid());
        }
    }
}
