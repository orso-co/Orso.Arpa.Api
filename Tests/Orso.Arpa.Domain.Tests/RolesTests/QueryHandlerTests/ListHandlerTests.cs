using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.RolesTests.QueryHandlerTests
{
    public class ListHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _roleManager = new FakeRoleManager();
            _handler = new Logic.Roles.List.Handler(_roleManager);
        }

        private RoleManager<Role> _roleManager;
        private Logic.Roles.List.Handler _handler;

        [Test]
        public async Task Should_Get_Role_List()
        {
            // Arrange
            var listQuery = new Logic.Roles.List.Query();
            IEnumerable<Role> expectedRoles = RoleSeedData.Roles;

            // Act
            IEnumerable<Role> roles = await _handler.Handle(listQuery, new CancellationToken());

            // Assert
            roles.Should().BeEquivalentTo(expectedRoles, opt => opt.Excluding(user => user.ConcurrencyStamp));
        }
    }
}
