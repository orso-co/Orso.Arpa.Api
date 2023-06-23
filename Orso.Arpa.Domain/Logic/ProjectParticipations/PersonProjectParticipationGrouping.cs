using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations;

public class PersonProjectParticipationGrouping
{
    public Project Project { get; set; }
    public IEnumerable<ProjectParticipation> ProjectParticipations { get; set; }
}
