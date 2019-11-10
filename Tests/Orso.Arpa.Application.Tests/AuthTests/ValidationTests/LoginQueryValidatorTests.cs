using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.AuthTests.ValidationTests
{
    [TestFixture]
    public class LoginQueryValidatorTests
    {

        private Login.QueryValidator _validator;
        private UserManager<User> _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new Login.QueryValidator(_userManager);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Email_Is_Supplied([Values(null, "", "test@")] string email)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.Email, email);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Email_Does_Not_Exist()
        {
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new Login.Query { Email = "test@test.de", Password = UserSeedData.ValidPassword });

            act.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Email_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.Email, UserSeedData.Orsianer.Email);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Invalid_Password_Is_Supplied([Values(null, "")] string password)
        {
            _validator.ShouldHaveValidationErrorFor(query => query.Password, password);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_Password_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(query => query.Password, UserSeedData.ValidPassword);
        }
    }
}
