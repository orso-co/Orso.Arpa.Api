using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations;

public class MusicianProfileProjectParticipationGrouping
{
    public Project Project { get; set; }
    public ProjectParticipation ProjectParticipation { get; set; }
    public MusicianProfile MusicianProfile { get; set; }
}
