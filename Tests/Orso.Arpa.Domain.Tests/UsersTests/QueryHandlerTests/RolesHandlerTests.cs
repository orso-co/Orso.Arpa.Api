using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.UsersTests.QueryHandlerTests
{
    public class RolesHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new Users.Roles.Handler(_userManager);
        }

        private UserManager<User> _userManager;
        private Users.Roles.Handler _handler;

        [Test]
        public async Task Should_Get_Roles()
        {
            // Arrange
            var rolesQuery = new Users.Roles.Query(UserSeedData.Orsianer);
            IEnumerable<string> expectedRoles = new List<string> { RoleNames.Orsianer };

            // Act
            IEnumerable<string> roles = await _handler.Handle(rolesQuery, new CancellationToken());

            // Assert
            roles.Should().BeEquivalentTo(expectedRoles);
        }
    }
}
