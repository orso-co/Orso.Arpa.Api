using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Repositories;
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
            _handler = new ListUserRoles.Handler(_userManager);
        }

        private ArpaUserManager _userManager;
        private ListUserRoles.Handler _handler;

        [Test]
        public async Task Should_Get_Roles()
        {
            // Arrange
            var rolesQuery = new ListUserRoles.Query(FakeUsers.Performer);
            IEnumerable<string> expectedRoles = new[] { RoleNames.Performer };

            // Act
            IEnumerable<string> roles = await _handler.Handle(rolesQuery, new CancellationToken());

            // Assert
            _ = roles.Should().BeEquivalentTo(expectedRoles);
        }
    }
}
