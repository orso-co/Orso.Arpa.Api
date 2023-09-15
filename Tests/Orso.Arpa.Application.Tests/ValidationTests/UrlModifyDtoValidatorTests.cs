using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class UrlModifyDtoValidatorTests
    {
        private UrlModifyDtoValidator _validator;
        private UrlModifyBodyDtoValidator _bodyValidator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UrlModifyDtoValidator();
            _bodyValidator = new UrlModifyBodyDtoValidator();
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Id_Is_Supplied()
        {
            await _validator.ShouldHaveValidationErrorForExactAsync(command => command.Id, Guid.Empty);
        }

        [Test]
        public async Task Should_Not_Have_Validation_Error_If_Valid_Id_Is_Supplied()
        {
            await _validator.ShouldNotHaveValidationErrorForExactAsync(command => command.Id, Guid.NewGuid());
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_Empty_Href_Is_Supplied([Values(null, "")] string name)
        {
            await _bodyValidator.ShouldHaveValidationErrorForExactAsync(dto => dto.Href, name);
        }
    }
}
