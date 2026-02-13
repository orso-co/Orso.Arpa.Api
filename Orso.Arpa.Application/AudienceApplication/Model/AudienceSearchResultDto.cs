using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.AudienceApplication.Model
{
    public class AudienceSearchResultDto
    {
        public int TotalCount { get; set; }
        public List<AudiencePersonDto> Items { get; set; } = [];
    }

    public class AudiencePersonDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string MainInstrument { get; set; }
        public List<string> AllInstruments { get; set; } = [];
        public bool HasAccount { get; set; }
        public string Email { get; set; }
        public byte Reliability { get; set; }
        public byte GeneralPreference { get; set; }
        public byte ExperienceLevel { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public byte ProfilePreferenceTeam { get; set; }
        public string ParticipationStatus { get; set; }
        public string InvitationStatus { get; set; }
    }
}
