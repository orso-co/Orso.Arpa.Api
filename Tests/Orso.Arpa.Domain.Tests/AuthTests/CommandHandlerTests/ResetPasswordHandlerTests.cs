using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Identity;

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

        private ArpaUserManager _userManager;
        private ResetPassword.Handler _handler;

        [Test]
        public async Task Should_Rest_Password()
        {
            // Arrange
            var command = new ResetPassword.Command
            {
                Token = "Token",
                Password = UserSeedData.ValidPassword,
                UsernameOrEmail = "ludmilla"
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
