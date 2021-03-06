using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Projects;
using Orso.Arpa.Persistence.Seed;

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
                    InvitationStatusId = SelectValueMappingSeedData.ProjectParticipationInvitationStatusMappings[0].Id,
                    ParticipationStatusInternalId = SelectValueMappingSeedData.ProjectParticipationStatusInternalMappings[0].Id,
                    ParticipationStatusInnerId = SelectValueMappingSeedData.ProjectParticipationStatusInnerMappings[1].Id,
                    CommentByStaffInner = "Comment by staff",
                    CommentTeam = "Comment by team"
                }, Guid.Parse("429ac181-9b36-4635-8914-faabc5f593ff"));
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
