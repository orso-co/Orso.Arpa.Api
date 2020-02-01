using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Application.Logic.Regions.Create;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RegionCreateDtoValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Validator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Name_Is_Supplied([Values(null, "")] string name)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Name, name);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Name_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Name, "Honolulu");
        }
    }
}
