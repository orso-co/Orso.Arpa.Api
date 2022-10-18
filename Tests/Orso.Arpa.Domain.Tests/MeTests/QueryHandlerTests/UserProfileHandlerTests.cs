using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.MeTests.QueryHandlerTests
{
    [TestFixture]
    public class UserProfileHandlerTests
    {
        private IUserAccessor _userAccessor;
        private UserProfile.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new UserProfile.Handler(_userAccessor);
        }

        [Test]
        public async Task Should_Get_Current_User_Profile()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _ = _userAccessor.GetCurrentUserAsync(Arg.Any<CancellationToken>()).Returns(user);

            // Act
            User result = await _handler.Handle(new UserProfile.Query(), new CancellationToken());

            // Assert
            _ = result.Should().BeEquivalentTo(user);
        }
    }
}
