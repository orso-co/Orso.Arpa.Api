using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public class ProjectParticipationChangedByPerformerTemplate : ITemplate
    {
        public string Name => "Project_Participation_Changed_By_Performer";
        public string ProjectName { get; set; }
        public string MusicianName { get; set; }
        public string ParticipationStatus { get; set; }
        public string Comment { get; set; }
        public string ParticipationStatusInternal { get; set; }
        public string CommentByStaff { get; set; }
    }
}
