using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.AuthTests.ValidationTests
{
    [TestFixture]
    public class SetRoleCommandValidatorTests
    {

        private SetRole.CommandValidator _validator;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _roleManager = new FakeRoleManager();
            _validator = new SetRole.CommandValidator(_userManager, _roleManager);
        }

        [Test]
        public void Should_Have_Validation_Error_If_Empty_UserName_Is_Supplied([Values(null, "")] string username)
        {
            _validator.ShouldHaveValidationErrorFor(command => command.UserName, username);
        }

        [Test]
        public void Should_Not_Have_Validation_Error_If_Empty_RoleName_Is_Supplied([Values(null, "")] string roleName)
        {
            _validator.ShouldNotHaveValidationErrorFor(command => command.RoleName, roleName);
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_Role_Does_Not_Exist()
        {
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new SetRole.Command { UserName = UserSeedData.Orsianer.UserName, RoleName = "DoesNotExist" });

            act.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_User_Does_Not_Exist()
        {
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new SetRole.Command { UserName = "DoesNotExist", RoleName = RoleNames.Orsianer });
            act.Should().Throw<RestException>();
        }
    }
}
