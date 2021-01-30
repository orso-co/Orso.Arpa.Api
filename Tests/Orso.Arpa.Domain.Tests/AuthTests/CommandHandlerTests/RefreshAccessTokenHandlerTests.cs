using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class RefreshAccessTokenHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new RefreshAccessToken.Handler(_userManager, _jwtGenerator, _arpaContext);
        }

        private UserManager<User> _userManager;
        private IJwtGenerator _jwtGenerator;
        private IArpaContext _arpaContext;
        private RefreshAccessToken.Handler _handler;

        [Test]
        public async Task Should_Refresh_AccessToken()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "performer_valid_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>()).Returns("newToken");

            // Act
            string result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo("newToken");
        }

        [Test]
        public void Should_Throw_ValidationException_If_No_User_Is_Found()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "Token",
                RemoteIpAddress = "127.0.0.1",
            };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>()).Returns("newToken");

            // Act
            Func<Task<string>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().Throw<ValidationException>();
        }

        [Test]
        public void Should_Throw_AuthenticationException_If_RefreshToken_Is_Expired()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "orsonaut_expired_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>()).Returns("newToken");

            // Act
            Func<Task<string>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().Throw<AuthenticationException>();
        }

        [Test]
        public void Should_Throw_AuthorizationException_If_RefreshToken_Is_Accessed_With_Different_Ip()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "performer_valid_refresh_token",
                RemoteIpAddress = "127.0.0.2",
            };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>()).Returns("newToken");

            // Act
            Func<Task<string>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().Throw<AuthorizationException>();
        }
    }
}
