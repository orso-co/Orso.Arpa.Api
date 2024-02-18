using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Misc;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Notifications
{
    public class MusicianProfileCreatedNotification : INotification
    {
        public MusicianProfile MusicianProfile { get; set; }
    }

    public class SendMusicianProfileCreatedInfoToKbb : INotificationHandler<MusicianProfileCreatedNotification>
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<SendMusicianProfileCreatedInfoToKbb> _logger;

        public SendMusicianProfileCreatedInfoToKbb(
            JwtConfiguration jwtConfiguration,
            IArpaContext arpaContext,
            ILogger<SendMusicianProfileCreatedInfoToKbb> logger)
        {
            _jwtConfiguration = jwtConfiguration;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(MusicianProfileCreatedNotification notification, CancellationToken cancellationToken)
        {
            Person person = notification.MusicianProfile.Person
                ?? await _arpaContext.Set<Person>().FindAsync([notification.MusicianProfile.PersonId], cancellationToken);
            var musicianProfileIdAsString = notification.MusicianProfile.Id.ToString("D");
            var personIdAsString = person.Id.ToString("D");

            KbbInfoLogger.LogInfoForKbb(
                _logger,
                "musician profile created",
                new Dictionary<string, object>
                {
                    { "Person", person },
                    { "Instrument", notification.MusicianProfile.Instrument ?? await _arpaContext.Set<Section>().FindAsync([notification.MusicianProfile.InstrumentId], cancellationToken) },
                    { "Level Assessment Inner", notification.MusicianProfile.LevelAssessmentInner },
                    { "Qualification", notification.MusicianProfile.Qualification != null
                        ? (await _arpaContext.Set<SelectValue>().FindAsync([notification.MusicianProfile.Qualification.SelectValueId], cancellationToken)).Name
                        : null },
                    { "Created by", notification.MusicianProfile.CreatedBy },
                    { "Link", $"{_jwtConfiguration.Audience}/arpa/mupro/{personIdAsString}/(projects//modal:{personIdAsString}/{musicianProfileIdAsString})?comboInstruments=true".FormatLink("Open Musician Profile") }
                });
        }
    }
}
