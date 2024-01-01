namespace Orso.Arpa.Mail.Templates
{
    public class ProjectParticipationChangedByStaffTemplate : BaseTemplate
    {
        public override string Name => "Project_Participation_Changed_By_Staff";
        public string ProjectName { get; set; }
        public string ParticipationStatusInner { get; set; }
        public string ParticipationStatusInternal { get; set; }
        public string Comment { get; set; }
        public string CommentByStaff { get; set; }
        public string InvitationStatus { get; set; }
    }
}
