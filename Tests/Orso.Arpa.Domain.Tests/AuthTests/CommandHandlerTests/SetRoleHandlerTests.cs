using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class SetRoleHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new SetRole.Handler(_userManager);
        }

        private ArpaUserManager _userManager;
        private SetRole.Handler _handler;

        [Test]
        public async Task Should_Set_Role_Return_False()
        {
            // Arrange
            User user = FakeUsers.Performer;
            var command = new SetRole.Command
            {
                Username = user.UserName,
                RoleNames = RoleNames.Staff
            };

            // Act
            bool result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task Should_Set_Role_Return_True()
        {
            // Arrange
            User user = FakeUsers.UserWithoutRole;
            var command = new SetRole.Command
            {
                Username = user.UserName,
                RoleNames = RoleNames.Performer
            };

            // Act
            bool result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeTrue();
        }
    }
}
