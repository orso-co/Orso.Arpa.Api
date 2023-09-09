using System.Collections.Generic;

namespace Orso.Arpa.Domain.ProjectDomain.Model;

public class PersonProjectParticipationGrouping
{
    public Project Project { get; set; }
    public IEnumerable<ProjectParticipation> ProjectParticipations { get; set; }
}
