using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Auth.SetRole;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SetRoleCommandValidatorTests
    {
        private Validator _validator;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _roleManager = new FakeRoleManager();
            _validator = new Validator(_userManager, _roleManager);
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_Role_Does_Not_Exist()
        {
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new Command { UserName = UserSeedData.Orsianer.UserName, RoleName = "DoesNotExist" });

            act.Should().Throw<RestException>();
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_User_Does_Not_Exist()
        {
            Func<Task<ValidationResult>> act = async () => await _validator
                .ValidateAsync(new Command { UserName = "DoesNotExist", RoleName = RoleNames.Orsianer });
            act.Should().Throw<RestException>();
        }
    }
}
