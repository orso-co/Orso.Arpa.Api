using System;
using Orso.Arpa.Domain.Logic.MyProjects;

namespace Orso.Arpa.Domain.Entities
{
    public class ProjectParticipation : BaseEntity
    {
        public ProjectParticipation(Logic.Projects.SetProjectParticipation.Command command, Guid? id = null) : base(id)
        {
            ProjectId = command.ProjectId;
            MusicianProfileId = command.MusicianProfileId;
            CommentByStaffInner = command.CommentByStaffInner;
            CommentTeam = command.CommentTeam;
            InvitationStatusId = command.InvitationStatusId;
            ParticipationStatusInnerId = command.ParticipationStatusInnerId;
            ParticipationStatusInternalId = command.ParticipationStatusInternalId;
        }

        protected ProjectParticipation() { }

        public void Update(SetProjectParticipationStatus.Command command)
        {
            ParticipationStatusInnerId = command.ParticipationStatusInnerId;
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        public void Update(Logic.Projects.SetProjectParticipation.Command command)
        {
            CommentByStaffInner = command.CommentByStaffInner;
            CommentTeam = command.CommentTeam;
            InvitationStatusId = command.InvitationStatusId;
            ParticipationStatusInnerId = command.ParticipationStatusInnerId;
            ParticipationStatusInternalId = command.ParticipationStatusInternalId;
        }

        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }

        public string CommentByPerformerInner { get; set; }
        public string CommentByStaffInner { get; set; }
        public string CommentTeam { get; set; }
        public Guid? ParticipationStatusInnerId { get; set; }
        public virtual SelectValueMapping ParticipationStatusInner { get; set; }
        public Guid? ParticipationStatusInternalId { get; set; }
        public virtual SelectValueMapping ParticipationStatusInternal { get; set; }
        public Guid? InvitationStatusId { get; set; }
        public virtual SelectValueMapping InvitationStatus { get; set; }
    }
}
