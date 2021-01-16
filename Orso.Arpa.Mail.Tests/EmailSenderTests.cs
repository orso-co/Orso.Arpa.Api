using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using netDumbster.smtp;
using NUnit.Framework;

namespace Orso.Arpa.Mail.Tests
{
    [TestFixture]
    public class EmailSenderTests
    {
        private EmailConfiguration _emailConfiguration;
        private SimpleSmtpServer _server;

        [SetUp]
        public void SetUp()
        {
            _server = Configuration.Configure()
                .WithPort(25)
                .Build();
            _emailConfiguration = new EmailConfiguration()
            {
                From = "sender@test.de",
                Port = _server.Configuration.Port,
                SmtpServer = "localhost"
            };
        }

        private EmailSender CreateEmailSender()
        {
            return new EmailSender(
                _emailConfiguration);
        }

        [Test]
        public async Task Should_Send_Email_Async()
        {
            // Arrange
            EmailSender emailSender = CreateEmailSender();
            var expectedRecipient = "recipient@test.de";
            var expectedSubject = "Expected subject";
            var expectedBody = "Expected body";
            var emailMessage = new EmailMessage(new string[] { expectedRecipient }, expectedSubject, expectedBody, false);

            // Act
            await emailSender.SendEmailAsync(
                emailMessage);

            // Assert
            _server.ReceivedEmailCount.Should().Be(1);
            SmtpMessage receivedMail = _server.ReceivedEmail.First();
            receivedMail.MessageParts[0].BodyData.Should().Be(expectedBody);
            receivedMail.ToAddresses.First().Address.Should().Be(expectedRecipient);
            receivedMail.Headers.Get("Subject").Should().Be(expectedSubject);
        }
    }
}
