using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.ValidatorTests
{
    [TestFixture]
    public class SetRoleCommandValidatorTests
    {
        private SetRole.Validator _validator;
        private ArpaUserManager _userManager;
        private RoleManager<Role> _roleManager;
        private static readonly string[] s_doesNotExistMessage = ["DoesNotExist"];


        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _roleManager = new FakeRoleManager();
            _validator = new SetRole.Validator(_userManager, _roleManager);
        }

        [Test]
        public async Task Should_Throw_Rest_Exception_If_Role_Does_Not_Exist()
        {
            await _validator.ShouldHaveNotFoundErrorFor(c => c.RoleNames, new SetRole.Command()
            {
                Username = UserTestSeedData.Staff.UserName,
                RoleNames = s_doesNotExistMessage

            }, typeof(Role).Name);
        }

        [Test]
        public async Task Should_Have_Validation_Error_If_User_Does_Not_Exist()
        {
            await _validator.ShouldHaveNotFoundErrorForAsync(c => c.Username, "DoesNotExist", typeof(User).Name);
        }
    }
}
