using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class ProjectParticipationSeedData
    {
        public static IList<ProjectParticipation> ProjectParticipations
        {
            get
            {
                return new List<ProjectParticipation>
                {
                    PerformerSchneeköniginParticipation,
                    PerformerRockingXMasParticipation,
                    PerformerHoorayForHollywoodParticipation,
                    PerformerChorwerkstattParticipation,
                    PerformerChorwerkstattFreiburgParticipation,
                    StaffParticipation1,
                    StaffParticipation2,
                    AdminParticipation
                };
            }
        }

        public static ProjectParticipation PerformerRockingXMasParticipation
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id
                }, Guid.Parse("2b3503d3-9061-4110-85e6-88e864842ece"));
            }
        }

        public static ProjectParticipation PerformerSchneeköniginParticipation
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.Schneekönigin.Id,
                    MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    InvitationStatus = ProjectInvitationStatus.Invited,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    CommentByStaffInner = "Comment by staff",
                    CommentTeam = "Comment by team"
                }, Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"));
            }
        }

        public static ProjectParticipation PerformerHoorayForHollywoodParticipation
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.HoorayForHollywood.Id,
                    MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    InvitationStatus = ProjectInvitationStatus.Invited,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Candidate,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Refusal,
                    CommentByStaffInner = "Comment by staff",
                    CommentTeam = "Comment by team"
                }, Guid.Parse("42fe1129-72f1-4935-b136-9bc41583e895"));
            }
        }

        public static ProjectParticipation PerformerChorwerkstattParticipation
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.Chorwerkstatt.Id,
                    MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    InvitationStatus = ProjectInvitationStatus.Invited,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Refusal,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    CommentByStaffInner = "Comment by staff",
                    CommentTeam = "Comment by team"
                }, Guid.Parse("014b7ae4-9c6a-4273-b54e-c40a911d41a3"));
            }
        }

        public static ProjectParticipation PerformerChorwerkstattFreiburgParticipation
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.ChorwerkstattFreiburg.Id,
                    MusicianProfileId = MusicianProfileSeedData.PerformerMusicianProfile.Id,
                    InvitationStatus = ProjectInvitationStatus.Invited,
                    ParticipationStatusInternal = ProjectParticipationStatusInternal.Acceptance,
                    ParticipationStatusInner = ProjectParticipationStatusInner.Acceptance,
                    CommentByStaffInner = "Comment by staff",
                    CommentTeam = "Comment by team"
                }, Guid.Parse("bd70283c-22ba-4ddb-9ae2-5f85d0151811"));
            }
        }

        public static ProjectParticipation StaffParticipation1
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.StaffMusicianProfile1.Id
                }, Guid.Parse("da5eb778-b907-45ce-955a-2af2e6c0b60f"));
            }
        }

        public static ProjectParticipation StaffParticipation2
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.StaffMusicianProfile2.Id
                }, Guid.Parse("4b9666cf-2d06-43cc-bd9f-f2d665562471"));
            }
        }

        public static ProjectParticipation AdminParticipation
        {
            get
            {
                return new ProjectParticipation(new SetProjectParticipation.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.AdminMusicianSopranoProfile.Id
                }, Guid.Parse("55cb5f7d-2fd7-4328-9d27-413dab753e62"));
            }
        }
    }
}
