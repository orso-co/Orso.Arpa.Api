using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Me;
using Orso.Arpa.Application.Logic.Users;
using Orso.Arpa.Application.Services;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

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
            _mediator.Send(Arg.Any<Domain.Logic.Users.List.Query>()).Returns(FakeUsers.Users.Where(u => !u.Deleted));
            _mediator.Send(Arg.Any<Domain.Logic.Users.Role.Query>()).Returns(
                RoleSeedData.Orsianer,
                RoleSeedData.Orsonaut,
                RoleSeedData.Orsoadmin,
                null);
            _mapper.Map<UserDto>(Arg.Any<User>()).Returns(UserDtoData.Orsianer, UserDtoData.Orsonaut, UserDtoData.Orsoadmin, UserDtoData.UserWithoutRole);

            // Act
            IEnumerable<UserDto> dtos = await _userService.GetAsync();

            // Assert
            dtos.Should().BeEquivalentTo(UserDtoData.Users);
        }

        [Test]
        public async Task Should_Get_Profile_Of_Current_User_Async()
        {
            // Arrange
            _mediator.Send(Arg.Any<Details.Query>()).Returns(Arpa.Tests.Shared.FakeData.FakeUsers.Orsianer);
            UserProfileDto expectedDto = FakerFabric.UesrProfileDtoFaker.Generate();
            _mapper.Map<UserProfileDto>(Arg.Any<User>()).Returns(expectedDto);

            // Act
            UserProfileDto profileDto = await _userService.GetProfileOfCurrentUserAsync();

            // Assert
            profileDto.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void Should_Delete()
        {
            // Act
            Func<Task> func = async () => await _userService.DeleteAsync("test");

            // Assert
            func.Should().NotThrow();
        }

        [Test]
        public void ModifyAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Logic.Me.Modify.Dto modifyDto = null;

            // Act
            Func<Task> func = async () => await _userService.ModifyProfileOfCurrentUserAsync(
               modifyDto);

            // Assert
            func.Should().NotThrow();
        }
    }
}
