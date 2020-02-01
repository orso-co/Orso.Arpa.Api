using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using static Orso.Arpa.Application.Logic.Regions.Modify;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class RegionModifyDtoValidatorTests
    {
        private Validator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Validator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorFor(command => command.Id, Guid.Empty);
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

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.Id, Guid.NewGuid());
        }
    }
}
