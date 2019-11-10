using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
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
            _mapper = Substitute.For<IMapper>();
            _handler = new Users.List.Handler(_userManager, _mapper);
        }

        private UserManager<User> _userManager;
        private IMapper _mapper;
        private Users.List.Handler _handler;

        [Test]
        public async Task Should_Get_User_List()
        {
            // Arrange
            var listQuery = new Users.List.Query();
            IList<UserDto> expectedDtos = UserDtoData.Users;
            var mappedOrsianer = new UserDto { DisplayName = UserSeedData.Orsianer.DisplayName, UserName = UserSeedData.Orsianer.UserName };
            var mappedOrsonaut = new UserDto { DisplayName = UserSeedData.Orsonaut.DisplayName, UserName = UserSeedData.Orsonaut.UserName };
            var mappedOrsoadmin = new UserDto { DisplayName = UserSeedData.Orsoadmin.DisplayName, UserName = UserSeedData.Orsoadmin.UserName };
            var mappedWithoutRole = new UserDto { DisplayName = UserSeedData.UserWithoutRole.DisplayName, UserName = UserSeedData.UserWithoutRole.UserName };
            _mapper.Map<UserDto>(Arg.Any<User>()).Returns(mappedOrsianer, mappedOrsonaut, mappedOrsoadmin, mappedWithoutRole);

            // Act
            IEnumerable<UserDto> dtos = await _handler.Handle(listQuery, new CancellationToken());

            // Assert
            dtos.Should().BeEquivalentTo(expectedDtos);
        }
    }
}
