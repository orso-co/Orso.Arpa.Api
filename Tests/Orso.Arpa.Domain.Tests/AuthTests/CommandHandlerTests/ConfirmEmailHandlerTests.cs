using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class ConfirmEmailHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _handler = new ConfirmEmail.Handler(_userManager);
        }

        private ArpaUserManager _userManager;
        private ConfirmEmail.Handler _handler;

        [Test]
        public async Task Should_Confirm_Email()
        {
            // Arrange
            var command = new ConfirmEmail.Command
            {
                Token = "ABCDEFGHIJKLMNOP+",
                Email = FakeUsers.UnconfirmedUser.Email,
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
