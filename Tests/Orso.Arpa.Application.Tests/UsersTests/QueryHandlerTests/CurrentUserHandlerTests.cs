using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Users;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.UsersTests.QueryHandlerTests
{
    [TestFixture]
    public class CurrentUserHandlerTests
    {
        private IMapper _mapper;
        private IUserAccessor _userAccessor;
        private CurrentUser.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _mapper = Substitute.For<IMapper>();
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new CurrentUser.Handler(_userAccessor, _mapper);
        }

        [Test]
        public async Task Should_Get_Current_User_Profile()
        {
            // Arrange
            User user = UserSeedData.Orsianer;
            _userAccessor.GetCurrentUserAsync().Returns(user);
            UserProfileDto expectedDto = UserProfileDtoData.Orsianer;
            _mapper.Map<UserProfileDto>(Arg.Any<User>()).Returns(expectedDto);

            // Act
            UserProfileDto result = await _handler.Handle(new CurrentUser.Query(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
        }
    }
}
