using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;
using Users = Orso.Arpa.Domain.Users;

namespace Orso.Arpa.Application.Tests.ServicesTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private IMapper _mapper;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();
            _userService = new UserService(_mediator, _mapper);
        }

        [Test]
        public async Task Should_Get_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Users.List.Query>()).Returns(UserSeedData.Users.Where(u => !u.Deleted));
            _mediator.Send(Arg.Any<Users.Roles.Query>()).Returns(
                new List<string> { RoleNames.Orsianer },
                new List<string> { RoleNames.Orsonaut },
                new List<string> { RoleNames.Orsoadmin },
                new List<string>());
            var mappedOrsianer = new UserDto
            {
                DisplayName = UserSeedData.Orsianer.DisplayName,
                UserName = UserSeedData.Orsianer.UserName
            };
            var mappedOrsonaut = new UserDto
            {
                DisplayName = UserSeedData.Orsonaut.DisplayName,
                UserName = UserSeedData.Orsonaut.UserName
            };
            var mappedOrsoadmin = new UserDto
            {
                DisplayName = UserSeedData.Orsoadmin.DisplayName,
                UserName = UserSeedData.Orsoadmin.UserName
            };
            var mappedWithoutRole = new UserDto
            {
                DisplayName = UserSeedData.UserWithoutRole.DisplayName,
                UserName = UserSeedData.UserWithoutRole.UserName
            };
            _mapper.Map<UserDto>(Arg.Any<User>()).Returns(mappedOrsianer, mappedOrsonaut, mappedOrsoadmin, mappedWithoutRole);

            // Act
            IEnumerable<UserDto> dtos = await _userService.GetAsync();

            // Assert
            dtos.Should().BeEquivalentTo(UserDtoData.Users);
        }

        [Test]
        public async Task Should_Get_Profile_Of_Current_User_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Users.CurrentUser.Query>()).Returns(UserSeedData.Orsianer);
            UserProfileDto expectedDto = UserProfileDtoData.Orsianer;
            _mapper.Map<UserProfileDto>(Arg.Any<User>()).Returns(expectedDto);

            // Act
            UserProfileDto profileDto = await _userService.GetProfileOfCurrentUserAsync();

            // Assert
            profileDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void Should_Delete_Async()
        {
            // Act
            Func<Task> func = async () => await _userService.DeleteAsync("test");

            // Assert
            func.Should().NotThrow();
        }
    }
}
