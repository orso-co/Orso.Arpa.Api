using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
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
            _handler = new LoginUser.Handler(_signInManager, _jwtGenerator, _identityConfiguration, _userManager);
        }

        private SignInManager<User> _signInManager;
        private ArpaUserManager _userManager;
        private IJwtGenerator _jwtGenerator;
        private IdentityConfiguration _identityConfiguration;
        private LoginUser.Handler _handler;

        [Test]
        public async Task Should_Login_User()
        {
            // Arrange
            const string expectedToken = "TestToken";
            User user = FakeUsers.Performer;
            var query = new LoginUser.Command { UsernameOrEmail = user.UserName, Password = UserSeedData.ValidPassword };
            _jwtGenerator.CreateTokensAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(expectedToken);

            // Act
            bool token = await _handler.Handle(query, new CancellationToken());

            // Assert
            token.Should();
        }

        [Test]
        public void Should_Throw_Authentication_Exception_If_Password_Is_not_Valid_Or_User_Is_Lockedout()
        {
            // Arrange
            User user = FakeUsers.Performer;
            var command = new LoginUser.Command { UsernameOrEmail = user.UserName, Password = "wrongpassword" };

            for (int i = 0; i < 3; i++)
            {
                // Act
                Func<Task> func = async () => await _handler.Handle(command, new CancellationToken());

                // Assert
                func.Should().ThrowAsync<AuthenticationException>().WithMessage("The system could not log you in. Please enter a valid user name and password");
            }

            // Act
            Func<Task> func1 = async () => await _handler.Handle(command, new CancellationToken());

            // Assert
            func1.Should().ThrowAsync<AuthorizationException>().WithMessage("Your account is locked out. Kindly wait for 10 minutes and try again");
        }
    }
}
