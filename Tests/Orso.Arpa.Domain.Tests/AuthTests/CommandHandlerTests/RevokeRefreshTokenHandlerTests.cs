using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class RevokeRefreshTokenHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new RevokeRefreshToken.Handler(_userManager, _arpaContext);
        }

        private UserManager<User> _userManager;
        private IArpaContext _arpaContext;
        private RevokeRefreshToken.Handler _handler;

        [Test]
        public async Task Should_Revoke_RefreshToken()
        {
            // Arrange
            var command = new RevokeRefreshToken.Command
            {
                RefreshToken = "orsianer_valid_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }

        [Test]
        public void Should_Throw_ValidationException_If_No_User_Is_Found()
        {
            // Arrange
            var command = new RevokeRefreshToken.Command
            {
                RefreshToken = "Token",
                RemoteIpAddress = "127.0.0.1",
            };

            // Act
            Func<Task<Unit>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().Throw<ValidationException>();
        }
    }
}
