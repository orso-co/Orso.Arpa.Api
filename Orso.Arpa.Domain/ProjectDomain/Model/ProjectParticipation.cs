using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Domain.ProjectDomain.Model
{
    public class ProjectParticipation : BaseEntity
    {
        public ProjectParticipation(SetProjectParticipation.Command command, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            ProjectId = command.ProjectId;
            MusicianProfileId = command.MusicianProfileId;
            CommentByStaffInner = command.CommentByStaffInner;
            CommentTeam = command.CommentTeam;
            InvitationStatus = command.InvitationStatus;
            ParticipationStatusInner = command.ParticipationStatusInner;
            ParticipationStatusInternal = command.ParticipationStatusInternal;
        }

        public ProjectParticipation(SetMyProjectParticipationStatus.Command command, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            ProjectId = command.ProjectId;
            MusicianProfileId = command.MusicianProfileId;
            CommentByPerformerInner = command.CommentByPerformerInner;
            ParticipationStatusInner = command.ParticipationStatusInner;
            ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate;
            InvitationStatus = ProjectInvitationStatus.Candidate;
        }

        internal ProjectParticipation(Project project, MusicianProfile musicianProfile) : base(Guid.Empty)
        {
            MusicianProfile = musicianProfile;
            Project = project;
            MusicianProfileId = musicianProfile.Id;
            ProjectId = project.Id;
        }

        protected ProjectParticipation() { }

        public void Update(SetMyProjectParticipationStatus.Command command)
        {
            ParticipationStatusInner = command.ParticipationStatusInner;
            CommentByPerformerInner = command.CommentByPerformerInner;
        }

        public void Update(SetProjectParticipation.Command command)
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
        public ProjectParticipationStatusInner? ParticipationStatusInner { get; set; }
        public ProjectParticipationStatusInternal? ParticipationStatusInternal { get; set; }
        public ProjectInvitationStatus? InvitationStatus { get; set; }

        [NotMapped]
        public ProjectParticipationStatusResult ParticipationStatusResult
        {
            get
            {
                if (HasOnlyAcceptanceStatus)
                {
                    return ProjectParticipationStatusResult.Acceptance;
                }
                if (HasAtLeastOneRefusalStatus)
                {
                    return ProjectParticipationStatusResult.Refusal;
                }
                return ProjectParticipationStatusResult.Pending;
            }
        }

        private bool HasAtLeastOneRefusalStatus =>
                ProjectParticipationStatusInner.Refusal.Equals(ParticipationStatusInner)
                || ProjectParticipationStatusInner.RehearsalsOnly.Equals(ParticipationStatusInner)
                || ProjectParticipationStatusInternal.Refusal.Equals(ParticipationStatusInternal);

        private bool HasOnlyAcceptanceStatus => ProjectParticipationStatusInner.Acceptance.Equals(ParticipationStatusInner)
                && ProjectParticipationStatusInternal.Acceptance.Equals(ParticipationStatusInternal);
    }
}
