using NUnit.Framework;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RegionCreateDtoValidatorTests
    {
        private RegionCreateDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RegionCreateDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorForExactAsync(command => command.Name, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Name, "Honolulu");
        }
    }
}
