using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.MediathekDomain.Notifications
{
    public class SendMediathekAccessGrantedMail : INotificationHandler<MediathekAccessGrantedNotification>
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ClubConfiguration _clubConfiguration;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SendMediathekAccessGrantedMail> _logger;

        public SendMediathekAccessGrantedMail(
            JwtConfiguration jwtConfiguration,
            ClubConfiguration clubConfiguration,
            IEmailSender emailSender,
            ILogger<SendMediathekAccessGrantedMail> logger)
        {
            _jwtConfiguration = jwtConfiguration;
            _clubConfiguration = clubConfiguration;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task Handle(MediathekAccessGrantedNotification notification, CancellationToken cancellationToken)
        {
            var person = notification.MediathekAccess.Person;
            if (person == null)
            {
                _logger.LogWarning("Cannot send mediathek access granted email: person not found on MediathekAccess");
                return;
            }

            var template = new MediathekAccessGrantedTemplate
            {
                ArpaLogo = _jwtConfiguration.ArpaLogo,
                DisplayName = person.DisplayName,
                MediathekUrl = _jwtConfiguration.Audience + "/#/user/mediathek",
                ClubAddress = _clubConfiguration.Address,
                ClubMail = _clubConfiguration.ContactEmail,
                ClubName = _clubConfiguration.Name,
                ClubPhoneNumber = _clubConfiguration.Phone
            };

            var emailAddress = person.GetPreferredEMailAddress();

            if (emailAddress != null)
            {
                await _emailSender.SendTemplatedEmailAsync(template, emailAddress);
            }
            else
            {
                _logger.LogError("Could not send the mediathek access granted email to {Person} because there is no email address assigned to this person.", person.DisplayName);
            }
        }
    }
}
