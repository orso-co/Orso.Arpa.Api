using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.AudienceApplication.Model
{
    public class AudienceSearchDto
    {
        public string SearchQuery { get; set; }
        public List<Guid> SectionIds { get; set; }
        public List<Guid> ProjectIds { get; set; }
        public string ProjectFilterOperator { get; set; } = "OR";
        public List<string> ParticipationStatuses { get; set; }
        public List<string> InvitationStatuses { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool? HasAccount { get; set; }
        public bool? HasMembership { get; set; }
        public List<Guid> MembershipStatusIds { get; set; }
        public List<Guid> SupportLevelIds { get; set; }
        public bool? MembershipActive { get; set; }
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
