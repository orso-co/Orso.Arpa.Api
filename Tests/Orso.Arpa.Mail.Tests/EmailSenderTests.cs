using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using netDumbster.smtp;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Mail.Tests
{
    [TestFixture]
    public class EmailSenderTests
    {
        private EmailConfiguration _emailConfiguration;
        private ITemplateParser _templateParser;
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
                SmtpServer = "localhost",
                DefaultSubject = "Message from arpa"
            };
            _templateParser = Substitute.For<ITemplateParser>();
        }

        [TearDown]
        public void StopServer()
        {
            if (_server != null)
            {
                _server.Stop();
                _server.Dispose();
            }
        }

        private EmailSender CreateEmailSender()
        {
            return new EmailSender(
                _emailConfiguration,
                _templateParser);
        }

        [Test]
        public async Task Should_Send_Email_Async()
        {
            // Arrange
            _server.ClearReceivedEmail();
            EmailSender emailSender = CreateEmailSender();
            var expectedRecipient = "recipient@test.de";
            var expectedSubject = "Expected subject";
            var expectedBody = "Expected body";
            var emailMessage = new EmailMessage(new string[] { expectedRecipient }, expectedSubject, expectedBody, null);

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

        [Test]
        public async Task Should_Send_Templated_Email_Async()
        {
            // Arrange
            _server.ClearReceivedEmail();
            EmailSender emailSender = CreateEmailSender();
            var confirmEmailTemplate = new ConfirmEmailTemplate();
            var expectedRecipient = "recipient@test.de";
            var expectedSubject = "Expected subject";
            var expectedBody = $"<title>{expectedSubject}</title>";
            _templateParser.Parse(Arg.Any<ITemplate>()).Returns(expectedBody);

            // Act
            await emailSender.SendTemplatedEmailAsync(confirmEmailTemplate, expectedRecipient);

            // Assert
            _server.ReceivedEmailCount.Should().Be(1);
            SmtpMessage receivedMail = _server.ReceivedEmail.First();
            receivedMail.MessageParts[0].BodyData.Should().Be(expectedBody);
            receivedMail.ToAddresses.First().Address.Should().Be(expectedRecipient);
            receivedMail.Headers.Get("Subject").Should().Be(expectedSubject);
        }

        [Test]
        public async Task Should_Send_Templated_Email_With_Default_Subject_Async()
        {
            // Arrange
            EmailSender emailSender = CreateEmailSender();
            var confirmEmailTemplate = new ConfirmEmailTemplate();
            var expectedRecipient = "recipient@test.de";
            var expectedSubject = _emailConfiguration.DefaultSubject;
            var expectedBody = "some body without title tags";
            _templateParser.Parse(Arg.Any<ITemplate>()).Returns(expectedBody);

            // Act
            await emailSender.SendTemplatedEmailAsync(confirmEmailTemplate, expectedRecipient);

            // Assert
            _server.ReceivedEmailCount.Should().Be(1);
            SmtpMessage receivedMail = _server.ReceivedEmail.First();
            receivedMail.MessageParts[0].BodyData.Should().Be(expectedBody);
            receivedMail.ToAddresses.First().Address.Should().Be(expectedRecipient);
            receivedMail.Headers.Get("Subject").Should().Be(expectedSubject);
        }
    }
}
