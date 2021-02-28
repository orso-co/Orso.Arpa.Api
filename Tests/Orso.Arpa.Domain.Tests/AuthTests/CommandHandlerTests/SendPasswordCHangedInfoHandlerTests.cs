using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    [TestFixture]
    public class SendPasswordChangedInfoHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _emailSender = Substitute.For<IEmailSender>();
            _jwtConfiguration = new JwtConfiguration();
            _clubConfiguration = new ClubConfiguration();
            _userAccessor = Substitute.For<IUserAccessor>();
            _handler = new SendPasswordChangedInfo.Handler(
                _userManager,
                _jwtConfiguration,
                _clubConfiguration,
                _emailSender,
                _userAccessor);
        }

        private ArpaUserManager _userManager;
        private IEmailSender _emailSender;
        private JwtConfiguration _jwtConfiguration;
        private ClubConfiguration _clubConfiguration;
        private IUserAccessor _userAccessor;
        private SendPasswordChangedInfo.Handler _handler;

        [Test]
        public async Task Should_Send_PasswordChanged_Info_With_Supplied_User()
        {
            var command = new SendPasswordChangedInfo.Command
            {
                UsernameOrEmail = UserTestSeedData.Performer.Email
            };

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }

        [Test]
        public async Task Should_Send_PasswordChanged_Info_Without_Supplied_User()
        {
            var command = new SendPasswordChangedInfo.Command();
            _userAccessor.GetCurrentUserAsync().Returns(FakeUsers.Performer);

            // Act
            Unit result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }
    }
}
