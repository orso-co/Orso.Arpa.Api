using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.UrlApplication.Model;
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
        public async Task Should_Have_Validation_Error_If_Empty_Href_Is_Supplied([Values(null, "")] string name)
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(command => command.Href, name);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_ProjectId_Is_Supplied()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Id, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_ProjectId_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, Guid.NewGuid());
        }
    }
}
