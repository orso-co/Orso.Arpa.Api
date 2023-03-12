using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Misc;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
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
                ?? await _arpaContext.Persons.FindAsync(new object[] { notification.MusicianProfile.PersonId }, cancellationToken);
            var musicianProfileIdAsString = notification.MusicianProfile.Id.ToString("D");
            var personIdAsString = person.Id.ToString("D");

            KbbInfoLogger.LogInfoForKbb(
                _logger,
                "musician profile created",
                new Dictionary<string, object>
                {
                    { "Person", person },
                    { "Instrument", notification.MusicianProfile.Instrument ?? await _arpaContext.Sections.FindAsync(new object[] { notification.MusicianProfile.InstrumentId }, cancellationToken) },
                    { "Level Assessment Inner", notification.MusicianProfile.LevelAssessmentInner },
                    { "Qualification", notification.MusicianProfile.Qualification != null
                        ? (await _arpaContext.SelectValues.FindAsync(new object[] { notification.MusicianProfile.Qualification.SelectValueId }, cancellationToken)).Name
                        : null },
                    { "Created by", notification.MusicianProfile.CreatedBy },
                    { "Link", $"{_jwtConfiguration.Audience}/arpa/mupro/{personIdAsString}/(projects//modal:{personIdAsString}/{musicianProfileIdAsString})?comboInstruments=true".FormatLink("Open Musician Profile") }
                });
        }
    }
}
