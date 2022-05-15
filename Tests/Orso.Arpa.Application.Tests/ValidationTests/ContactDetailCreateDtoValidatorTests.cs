using FluentValidation.TestHelper;
using NUnit.Framework;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class ContactDetailCreateDtoValidatorTests
    {
        private readonly ContactDetailCreateBodyDtoValidator _bodyValidator = new();

        [Test]
        public void Should_Fail_If_Invalid_PhoneNumber_Is_Supplied([Values("123456789", "(0761) 12345678", "Invalid PhoneNumber")] string phoneNumber)
        {
            _bodyValidator.ShouldHaveValidationErrorForExact(dto => dto.Value, new ContactDetailCreateBodyDto
            {
                Key = ContactDetailKey.PhoneNumber,
                Value = phoneNumber
            }).WithErrorMessage("The supplied value is not a valid phone number");
        }

        [Test]
        public void Should_Succeed_If_Valid_PhoneNumber_Is_Supplied([Values("076615063", "0176-84784414", "0761 472141", "+49 173 5441838", "0172 / 1300800", "0041765801357")] string phoneNumber)
        {
            _bodyValidator.ShouldNotHaveValidationErrorForExact(dto => dto.Value, new ContactDetailCreateBodyDto
            {
                Key = ContactDetailKey.PhoneNumber,
                Value = phoneNumber
            });
        }
    }
}
