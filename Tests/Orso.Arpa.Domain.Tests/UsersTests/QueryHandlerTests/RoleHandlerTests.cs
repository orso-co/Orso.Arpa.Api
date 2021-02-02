using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.UsersTests.QueryHandlerTests
{
    public class RoleHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _roleManager = new FakeRoleManager();
            _handler = new Logic.Users.Role.Handler(_userManager, _roleManager);
        }

        private ArpaUserManager _userManager;
        private RoleManager<Domain.Entities.Role> _roleManager;
        private Logic.Users.Role.Handler _handler;

        [Test]
        public async Task Should_Get_Roles()
        {
            // Arrange
            var rolesQuery = new Logic.Users.Role.Query(FakeUsers.Performer);
            Entities.Role expectedRole = RoleSeedData.Performer;

            // Act
            Entities.Role role = await _handler.Handle(rolesQuery, new CancellationToken());

            // Assert
            role.Should().BeEquivalentTo(expectedRole, opt => opt.Excluding(r => r.ConcurrencyStamp));
        }
    }
}
