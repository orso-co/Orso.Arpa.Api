using System.Threading.Tasks;
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
        public async Task Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Name, name);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Name, "Honolulu");
        }
    }
}
