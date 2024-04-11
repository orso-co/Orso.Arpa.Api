using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.FakeData;
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
            _cookieSignIn = Substitute.For<ICookieSignIn>();
            _handler = new RefreshAccessToken.Handler(_userManager, _jwtGenerator, _arpaContext, _cookieSignIn, new FakeDateTimeProvider());
        }

        private ArpaUserManager _userManager;
        private IJwtGenerator _jwtGenerator;
        private IArpaContext _arpaContext;
        private RefreshAccessToken.Handler _handler;
        private ICookieSignIn _cookieSignIn;

        [Test]
        public async Task Should_Refresh_AccessToken()
        {
            // Arrange
            User user = FakeUsers.Performer;
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "performer_valid_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };

            // Act
            bool result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeTrue();
            await _jwtGenerator.Received().CreateRefreshTokenAsync(Arg.Is<User>(u => u.Email == user.Email), Arg.Any<string>(), Arg.Any<CancellationToken>());
            await _cookieSignIn.Received().AsyncRefreshSignIn(Arg.Is<User>(u => u.Email == user.Email));
        }

        [Test]
        public async Task Should_Throw_ValidationException_If_No_User_Is_Found()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "Token",
                RemoteIpAddress = "127.0.0.1",
            };

            // Act
            Func<Task<bool>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            _ = func.Should().ThrowAsync<ValidationException>();
            await _cookieSignIn.DidNotReceive().AsyncRefreshSignIn(Arg.Any<User>());
            await _jwtGenerator.DidNotReceive().CreateRefreshTokenAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Should_Throw_AuthenticationException_If_RefreshToken_Is_Expired()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "staff_expired_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };

            // Act
            Func<Task<bool>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            _ = func.Should().ThrowAsync<AuthenticationException>();
            await _cookieSignIn.DidNotReceive().AsyncRefreshSignIn(Arg.Any<User>());
            await _jwtGenerator.DidNotReceive().CreateRefreshTokenAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task Should_Throw_AuthorizationException_If_RefreshToken_Is_Accessed_With_Different_Ip()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "performer_valid_refresh_token",
                RemoteIpAddress = "127.0.0.2",
            };

            // Act
            Func<Task<bool>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            _ = func.Should().ThrowAsync<AuthorizationException>();
            await _cookieSignIn.DidNotReceive().AsyncRefreshSignIn(Arg.Any<User>());
            await _jwtGenerator.DidNotReceive().CreateRefreshTokenAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        }
    }
}
