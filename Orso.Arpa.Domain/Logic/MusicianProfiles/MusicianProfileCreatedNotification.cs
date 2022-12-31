using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public class MusicianProfileCreatedNotification : INotification
    {
        public MusicianProfile MusicianProfile { get; set; }
    }

    public class SendMusicianProfileCreatedInfo : INotificationHandler<MusicianProfileCreatedNotification>
    {
        private readonly ClubConfiguration _clubConfiguration;
        private readonly IEmailSender _emailSender;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IArpaContext _arpaContext;

        public SendMusicianProfileCreatedInfo(
            ClubConfiguration clubConfiguration,
            IEmailSender emailSender,
            JwtConfiguration jwtConfiguration,
            IArpaContext arpaContext)
        {
            _clubConfiguration = clubConfiguration;
            _emailSender = emailSender;
            _jwtConfiguration = jwtConfiguration;
            _arpaContext = arpaContext;
        }

        public async Task Handle(MusicianProfileCreatedNotification notification, CancellationToken cancellationToken)
        {
            Person person = notification.MusicianProfile.Person
                ?? await _arpaContext.Persons.FindAsync(new object[] { notification.MusicianProfile.PersonId }, cancellationToken);

            var template = new MusicianProfileCreatedTemplate
            {
                ArpaLogo = $"{_jwtConfiguration.Audience}/assets/common/logos/arpa_logo.png",
                DisplayName = person.DisplayName,
                Instrument = (await _arpaContext.Sections.FindAsync(new object[] { notification.MusicianProfile.InstrumentId }, cancellationToken)).Name,
                LevelAssessmentInner = notification.MusicianProfile.LevelAssessmentInner.ToString(),
                Qualification = notification.MusicianProfile.Qualification != null
                    ? (await _arpaContext.SelectValues.FindAsync(new object[] { notification.MusicianProfile.Qualification.SelectValueId }, cancellationToken)).Name
                    : "- no qualification selected -",
                Id = notification.MusicianProfile.Id.ToString("D"),
                ClubAddress = _clubConfiguration.Address,
                ClubMail = _clubConfiguration.ContactEmail,
                ClubName = _clubConfiguration.Name,
                ClubPhoneNumber = _clubConfiguration.Phone
            };

            await _emailSender.SendTemplatedEmailAsync(template, _clubConfiguration.InternalEmail);
        }
    }
}
