using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class LoginHandlerTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _signInManager = new FakeSignInManager();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _identityConfiguration = new IdentityConfiguration() { LockoutExpiryInMinutes = 10 };
            _cookieSignIn = Substitute.For<ICookieSignIn>();
            _handler = new LoginUser.Handler(_signInManager, _jwtGenerator, _identityConfiguration, _userManager, _cookieSignIn);
        }

        private SignInManager<User> _signInManager;
        private ArpaUserManager _userManager;
        private IJwtGenerator _jwtGenerator;
        private IdentityConfiguration _identityConfiguration;
        private LoginUser.Handler _handler;
        private ICookieSignIn _cookieSignIn;


        [Test, Order(1)]
        public async Task Should_Login_User()
        {
            // Arrange
            User user = FakeUsers.Performer;
            var query = new LoginUser.Command { UsernameOrEmail = user.UserName, Password = UserSeedData.ValidPassword };
            _cookieSignIn.IsCookieSignInPossible(Arg.Any<User>(), Arg.Any<string>()).Returns(true);

            // Act
            bool result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.Should().BeTrue();
            await _cookieSignIn.Received().IsCookieSignInPossible(Arg.Is<User>(u => u.Email == user.Email), Arg.Any<string>());
            await _cookieSignIn.Received().SignInUser(Arg.Is<User>(u => u.Email == user.Email));
            await _jwtGenerator.Received().CreateRefreshTokenAsync(Arg.Is<User>(u => u.Email == user.Email), Arg.Any<string>(), Arg.Any<CancellationToken>());
        }

        [Test, Order(2)]
        public async Task Should_Throw_Authentication_Exception_If_Password_Is_not_Valid_Or_User_Is_Lockedout()
        {
            // Arrange
            User user = FakeUsers.Performer;
            var command = new LoginUser.Command { UsernameOrEmail = user.UserName, Password = "wrongpassword" };

            for (int i = 0; i < 3; i++)
            {
                // Act
                Func<Task> func = async () => await _handler.Handle(command, new CancellationToken());

                // Assert
                _ = func.Should().ThrowAsync<AuthenticationException>().WithMessage("The system could not log you in. Please enter a valid user name and password");
                await _cookieSignIn.Received().IsCookieSignInPossible(Arg.Is<User>(u => u.Email == user.Email), Arg.Any<string>());
                await _cookieSignIn.DidNotReceive().SignInUser(Arg.Any<User>());
            }

            // Act
            Func<Task> func1 = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            _ = func1.Should().ThrowAsync<AuthorizationException>().WithMessage("Your account is locked out. Kindly wait for 10 minutes and try again");
            await _cookieSignIn.Received().IsCookieSignInPossible(Arg.Is<User>(u => u.Email == user.Email), Arg.Any<string>());
            await _cookieSignIn.DidNotReceive().SignInUser(Arg.Any<User>());
        }
    }
}
