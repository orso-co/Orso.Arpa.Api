using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.UsersTests.QueryHandlerTests
{
    public class UserListHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new Domain.Users.List.Handler(_userManager);
        }

        private UserManager<User> _userManager;
        private Domain.Users.List.Handler _handler;

        [Test]
        public async Task Should_Get_User_List()
        {
            // Arrange
            var listQuery = new Domain.Users.List.Query();
            IList<User> expectedUsers = UserSeedData.Users;

            // Act
            IEnumerable<User> users = await _handler.Handle(listQuery, new CancellationToken());

            // Assert
            users.Should().BeEquivalentTo(expectedUsers);
        }
    }
}
