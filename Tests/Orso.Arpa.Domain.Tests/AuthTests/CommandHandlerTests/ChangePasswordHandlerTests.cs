using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class ChangePasswordHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new ChangePassword.Handler(_userAccessor, _userManager);
        }

        private ArpaUserManager _userManager;
        private IUserAccessor _userAccessor;
        private ChangePassword.Handler _handler;

        [Test]
        public async Task Should_Change_Password()
        {
            // Arrange
            User user = Arpa.Tests.Shared.FakeData.FakeUsers.Performer;
            var command = new ChangePassword.Command
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = "NewPa$$w0rd"
            };
            _userAccessor.GetCurrentUserAsync().Returns(user);

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
