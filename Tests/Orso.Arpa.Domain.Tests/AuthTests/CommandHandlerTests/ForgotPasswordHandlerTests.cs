using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class ForgotPasswordHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _emailSender = Substitute.For<IEmailSender>();
            _jwtConfiguration = new JwtConfiguration();
            _clubConfiguration = new ClubConfiguration();
            _handler = new ForgotPassword.Handler(_userManager, _clubConfiguration, _jwtConfiguration, _emailSender);
        }

        private UserManager<User> _userManager;
        private IEmailSender _emailSender;
        private JwtConfiguration _jwtConfiguration;
        private ClubConfiguration _clubConfiguration;
        private ForgotPassword.Handler _handler;

        [Test]
        public async Task Should_Handle_Forgot_Password()
        {
            // Arrange
            User user = Arpa.Tests.Shared.FakeData.FakeUsers.Performer;
            var command = new ForgotPassword.Command
            {
                UserName = user.UserName,
                ClientUri = "http://localhost:4200"
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
