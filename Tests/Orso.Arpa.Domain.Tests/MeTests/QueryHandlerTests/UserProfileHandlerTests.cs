using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Queries;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.MeTests.QueryHandlerTests
{
    [TestFixture]
    public class UserProfileHandlerTests
    {
        private IUserAccessor _userAccessor;
        private MyUserProfile.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new MyUserProfile.Handler(_userAccessor);
        }

        [Test]
        public async Task Should_Get_Current_User_Profile()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _ = _userAccessor.GetCurrentUserAsync(Arg.Any<CancellationToken>()).Returns(user);

            // Act
            User result = await _handler.Handle(new MyUserProfile.Query(), new CancellationToken());

            // Assert
            _ = result.Should().BeEquivalentTo(user);
        }
    }
}
