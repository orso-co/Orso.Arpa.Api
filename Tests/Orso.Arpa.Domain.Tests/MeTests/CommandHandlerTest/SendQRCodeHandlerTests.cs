using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Mail;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHandlerTests
{
    public class SendQRCodeHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _emailSender = Substitute.For<IEmailSender>();
            _jwtConfiguration = new JwtConfiguration();
            _clubConfiguration = new ClubConfiguration();
            _handler = new SendQRCode.Handler(_userManager, _jwtConfiguration, _clubConfiguration, _emailSender);
        }

        private ArpaUserManager _userManager;
        private IEmailSender _emailSender;
        private JwtConfiguration _jwtConfiguration;
        private ClubConfiguration _clubConfiguration;
        private SendQRCode.Handler _handler;

        [Test]
        public async Task Should_Send_QRCode()
        {
            // Arrange
            var expectedFileName = "ARPA_QRCode_Per_Former.png";
            var expectedFile = File.ReadAllBytes(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Files",
                expectedFileName));
            var expectedQrCodeFile = new SendQRCode.QrCodeFile
            {
                Content = expectedFile,
                FileName = expectedFileName
            };

            var command = new SendQRCode.Command
            {
                Username = UserSeedData.Performer.UserName
            };

            // Act
            SendQRCode.QrCodeFile result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedQrCodeFile);
            await _emailSender.Received().SendTemplatedEmailAsync(Arg.Any<ITemplate>(), Arg.Any<string>(), Arg.Any<IList<EmailAttachment>>());
        }
    }
}
