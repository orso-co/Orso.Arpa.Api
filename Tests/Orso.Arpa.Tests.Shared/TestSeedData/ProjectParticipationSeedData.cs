using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.ProjectParticipations;

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
                    OrsianerParticipation,
                    OrsonautParticipation1,
                    OrsonautParticipation2,
                    OrsoadminParticipation
                };
            }
        }

        public static ProjectParticipation OrsianerParticipation
        {
            get
            {
                return new ProjectParticipation(Guid.Parse("2b3503d3-9061-4110-85e6-88e864842ece"), new Create.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.OrsianerMusicianProfile.Id
                });
            }
        }

        public static ProjectParticipation OrsonautParticipation1
        {
            get
            {
                return new ProjectParticipation(Guid.Parse("da5eb778-b907-45ce-955a-2af2e6c0b60f"), new Create.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.OrsonautMusicianProfile1.Id
                });
            }
        }

        public static ProjectParticipation OrsonautParticipation2
        {
            get
            {
                return new ProjectParticipation(Guid.Parse("4b9666cf-2d06-43cc-bd9f-f2d665562471"), new Create.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.OrsonautMusicianProfile2.Id
                });
            }
        }

        public static ProjectParticipation OrsoadminParticipation
        {
            get
            {
                return new ProjectParticipation(Guid.Parse("55cb5f7d-2fd7-4328-9d27-413dab753e62"), new Create.Command
                {
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                    MusicianProfileId = MusicianProfileSeedData.OrsoadminMusicianProfile1.Id
                });
            }
        }
    }
}
