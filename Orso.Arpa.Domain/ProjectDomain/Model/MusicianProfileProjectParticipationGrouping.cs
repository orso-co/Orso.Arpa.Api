using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Model;

public class MusicianProfileProjectParticipationGrouping
{
    public Project Project { get; set; }
    public ProjectParticipation ProjectParticipation { get; set; }
    public MusicianProfile MusicianProfile { get; set; }
}
