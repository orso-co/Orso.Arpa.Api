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
            _cookieSignInService = Substitute.For<ICookieSignInService>();
            _handler = new RefreshAccessToken.Handler(_userManager, _jwtGenerator, _arpaContext, _cookieSignInService, new FakeDateTimeProvider());
        }

        private ArpaUserManager _userManager;
        private IJwtGenerator _jwtGenerator;
        private IArpaContext _arpaContext;
        private RefreshAccessToken.Handler _handler;
        private ICookieSignInService _cookieSignInService;

        [Test]
        public async Task Should_Refresh_AccessToken()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "performer_valid_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns("newToken");
            _cookieSignInService.RefreshSignIn(Arg.Any<string>()).Returns(Task.CompletedTask);

            // Act
            bool result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeTrue();
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
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns("newToken");

            // Act
            Func<Task<bool>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public void Should_Throw_AuthenticationException_If_RefreshToken_Is_Expired()
        {
            // Arrange
            var command = new RefreshAccessToken.Command
            {
                RefreshToken = "staff_expired_refresh_token",
                RemoteIpAddress = "127.0.0.1",
            };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns("newToken");

            // Act
            Func<Task<bool>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().ThrowAsync<AuthenticationException>();
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
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns("newToken");

            // Act
            Func<Task<bool>> func = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func.Should().ThrowAsync<AuthorizationException>();
        }
    }
}
