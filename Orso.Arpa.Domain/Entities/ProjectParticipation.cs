using System;
using Orso.Arpa.Domain.Enums;
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
            InvitationStatus = command.InvitationStatus;
            ParticipationStatusInner = command.ParticipationStatusInner;
            ParticipationStatusInternal = command.ParticipationStatusInternal;
        }

        protected ProjectParticipation() { }

        public void Update(SetProjectParticipationStatus.Command command)
        {
            ParticipationStatusInner = command.ParticipationStatusInner;
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        public void Update(Logic.Projects.SetProjectParticipation.Command command)
        {
            CommentByStaffInner = command.CommentByStaffInner;
            CommentTeam = command.CommentTeam;
            InvitationStatus = command.InvitationStatus;
            ParticipationStatusInner = command.ParticipationStatusInner;
            ParticipationStatusInternal = command.ParticipationStatusInternal;
        }

        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }

        public string CommentByPerformerInner { get; set; }
        public string CommentByStaffInner { get; set; }
        public string CommentTeam { get; set; }

        [Obsolete("is only needed for migration purposes")]
        public Guid? ParticipationStatusInnerId { get; set; }
        public ProjectParticipationStatusInner? ParticipationStatusInner { get; set; }

        [Obsolete("is only needed for migration purposes")]
        public Guid? ParticipationStatusInternalId { get; set; }
        public ProjectParticipationStatusInternal? ParticipationStatusInternal { get; set; }

        [Obsolete("is only needed for migration purposes")]
        public Guid? InvitationStatusId { get; set; }
        public ProjectInvitationStatus? InvitationStatus { get; set; }
    }
}
