using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class ResetPasswordHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new ResetPassword.Handler(_userManager);
        }

        private UserManager<User> _userManager;
        private ResetPassword.Handler _handler;

        [Test]
        public async Task Should_Rest_Password()
        {
            // Arrange
            var command = new ResetPassword.Command
            {
                Token = "Token",
                Password = UserSeedData.ValidPassword,
                UserName = "ludmilla"
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
