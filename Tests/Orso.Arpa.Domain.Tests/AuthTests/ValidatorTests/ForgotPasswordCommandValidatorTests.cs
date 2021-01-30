using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.ForgotPassword;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class ForgotPasswordCommandValidatorTests
    {
        private Validator _validator;
        private UserManager<User> _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _validator = new Validator(_userManager);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Username_Does_Not_Exist()
        {
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new Command { UserName = "test" });

            act.Should().Throw<NotFoundException>();
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Valid_UserName_Is_Supplied()
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.UserName, UserSeedData.Performer.UserName);
        }
    }
}
