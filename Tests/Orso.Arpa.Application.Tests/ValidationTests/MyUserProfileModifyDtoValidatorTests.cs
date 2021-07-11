using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Application.Tests.ValidationTests
{
    [TestFixture]
    public class MyUserProfileModifyDtoValidatorTests
    {
        private MyUserProfileModifyDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MyUserProfileModifyDtoValidator();
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Email, email);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Email, "ludmilla@test.com");
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_GivenName_Is_Supplied([Values(null, "")] string givenName)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.GivenName, givenName);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_Surname_Is_Supplied([Values(null, "")] string surname)
        {
            _validator.ShouldHaveValidationErrorForExact(command => command.Surname, surname);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_GivenName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.GivenName, "Ludmilla");
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Surname_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorForExact(command => command.Surname, "Schneider");
        }
    }
}
