using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public class ProjectParticipationChangedByStaffTemplate : ITemplate
    {
        public string Name => "Project_Participation_Changed_By_Staff";
        public string ProjectName { get; set; }
        public string MusicianName { get; set; }
        public string ParticipationStatus { get; set; }
        public string Comment { get; set; }
        public string CommentByStaff { get; set; }
    }
}
