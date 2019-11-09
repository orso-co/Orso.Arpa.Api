using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Application.Tests.UsersTests.QueryHandlerTests
{
    public class UserListHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new Users.List.Handler(_userManager);
        }

        private UserManager<User> _userManager;
        private Users.List.Handler _handler;

        [Test]
        public async Task Should_Get_User_List()
        {
            // Arrange
            var listQuery = new Users.List.Query();
            IList<UserDto> expectedDtos = UserDtoData.Users;

            // Act
            IEnumerable<UserDto> dtos = await _handler.Handle(listQuery, new CancellationToken());

            // Assert
            dtos.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
