using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.UsersTests.QueryHandlerTests
{
    public class UserListHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new Logic.Users.List.Handler(_userManager);
        }

        private UserManager<User> _userManager;
        private Logic.Users.List.Handler _handler;

        [Test]
        public async Task Should_Get_User_List()
        {
            // Arrange
            var listQuery = new Logic.Users.List.Query();
            IEnumerable<User> expectedUsers = FakeUsers.Users.Where(u => !u.Deleted);

            // Act
            IEnumerable<User> users = await _handler.Handle(listQuery, new CancellationToken());

            // Assert
            users.Should().BeEquivalentTo(expectedUsers, opt => opt
                .Excluding(user => user.ConcurrencyStamp)
                .Excluding(user => user.RefreshTokens));
        }
    }
}
