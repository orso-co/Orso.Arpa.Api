using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Users;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.UsersTests.QueryHandlerTests
{
    [TestFixture]
    public class CurrentUserHandlerTests
    {
        private IUserAccessor _userAccessor;
        private CurrentUser.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new CurrentUser.Handler(_userAccessor);
        }

        [Test]
        public async Task Should_Get_Current_User_Profile()
        {
            // Arrange
            User user = UserSeedData.Orsianer;
            _userAccessor.GetCurrentUserAsync().Returns(user);

            // Act
            User result = await _handler.Handle(new CurrentUser.Query(), new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(user);
        }
    }
}
