using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Queries;
using Orso.Arpa.Domain.UserDomain.Repositories;
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
            _handler = new ListUsers.Handler(_userManager);
        }

        private ArpaUserManager _userManager;
        private ListUsers.Handler _handler;

        [Test]
        public async Task Should_Get_User_List()
        {
            // Arrange
            var listQuery = new ListUsers.Query();
            IList<User> expectedUsers = FakeUsers.Users.ToList();

            // Act
            IList<User> users = (await _handler.Handle(listQuery, new CancellationToken())).ToList();

            // Assert
            _ = users.Should().BeEquivalentTo(expectedUsers, opt => opt
                .Excluding(user => user.ConcurrencyStamp)
                .Excluding(user => user.RefreshTokens)
                .Excluding(user => user.Person));
        }
    }
}
