using System;
using NUnit.Framework;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UrlCreateDtoValidatorTests
    {
        private UrlCreateDtoValidator _validator;
        private UrlCreateBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UrlCreateDtoValidator();
            _bodyValidator = new UrlCreateBodyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Href_Is_Supplied([Values(null, "")] string name)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(command => command.Href, name);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_ProjectId_Is_Supplied()
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Id, Guid.Empty);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_ProjectId_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Id, Guid.NewGuid());
        }
    }
}
