using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
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

        private UserManager<User> _userManager;
        private SetRole.Handler _handler;

        [Test]
        public async Task Should_Set_Role()
        {
            // Arrange
            User user = FakeUsers.Orsianer;
            var command = new SetRole.Command
            {
                UserName = user.UserName,
                RoleName = RoleNames.Orsonaut
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
