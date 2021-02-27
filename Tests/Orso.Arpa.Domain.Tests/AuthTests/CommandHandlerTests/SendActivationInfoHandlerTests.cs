using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class SendActivationInfoHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _emailSender = Substitute.For<IEmailSender>();
            _jwtConfiguration = new JwtConfiguration();
            _clubConfiguration = new ClubConfiguration();
            _handler = new SendActivationInfo.Handler(_userManager, _jwtConfiguration, _clubConfiguration, _emailSender);
        }

        private ArpaUserManager _userManager;
        private IEmailSender _emailSender;
        private JwtConfiguration _jwtConfiguration;
        private ClubConfiguration _clubConfiguration;
        private SendActivationInfo.Handler _handler;

        [Test]
        public async Task Should_Send_Activation_Info()
        {
            var command = new SendActivationInfo.Command
            {
                Username = UserTestSeedData.Performer.UserName
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
