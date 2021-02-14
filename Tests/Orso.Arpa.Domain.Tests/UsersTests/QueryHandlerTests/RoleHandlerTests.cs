using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Roles;
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
            _handler = new Logic.Users.UserRoles.Handler(_userManager);
        }

        private ArpaUserManager _userManager;
        private Logic.Users.UserRoles.Handler _handler;

        [Test]
        public async Task Should_Get_Roles()
        {
            // Arrange
            var rolesQuery = new Logic.Users.UserRoles.Query(FakeUsers.Performer);
            IEnumerable<string> expectedRoles = new[] { RoleNames.Performer };

            // Act
            IEnumerable<string> roles = await _handler.Handle(rolesQuery, new CancellationToken());

            // Assert
            roles.Should().BeEquivalentTo(expectedRoles);
        }
    }
}
