using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.AudienceApplication.Model
{
    public class AudienceSearchDto
    {
        public string SearchQuery { get; set; }
        public List<Guid> SectionIds { get; set; }
        public Guid? ProjectId { get; set; }
        public List<string> ParticipationStatuses { get; set; }
        public List<string> InvitationStatuses { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ScoreFilterDto ScoreFilter { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 50;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }

    public class ScoreFilterDto
    {
        public int? MinReliability { get; set; }
        public int? MaxReliability { get; set; }
        public int? MinGeneralPreference { get; set; }
        public int? MaxGeneralPreference { get; set; }
        public int? MinExperienceLevel { get; set; }
        public int? MaxExperienceLevel { get; set; }
        public int? MinLevelAssessmentTeam { get; set; }
        public int? MaxLevelAssessmentTeam { get; set; }
        public int? MinProfilePreferenceTeam { get; set; }
        public int? MaxProfilePreferenceTeam { get; set; }
    }
}
