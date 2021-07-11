using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;
using static Orso.Arpa.Domain.Logic.Auth.SetRole;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SetRoleCommandValidatorTests
    {
        private Validator _validator;
        private ArpaUserManager _userManager;
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
            _validator.ShouldHaveNotFoundErrorFor(c => c.RoleNames, new Command()
            {
                Username = UserTestSeedData.Staff.UserName,
                RoleNames = new[] { "DoesNotExist" }
            }, typeof(Role).Name);
        }

        [Test]
        public void Should_Have_Validation_Error_If_User_Does_Not_Exist()
        {
            _validator.ShouldHaveNotFoundErrorFor(c => c.Username, "DoesNotExist", typeof(User).Name);
        }
    }
}
