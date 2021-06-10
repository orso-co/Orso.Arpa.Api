using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeProjectParticipations
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

        public static ProjectParticipation PerformerSchneeköniginParticipation
        {
            get
            {
                ProjectParticipation participation = ProjectParticipationSeedData.PerformerSchneeköniginParticipation;
                participation.SetProperty(nameof(ProjectParticipation.Project), FakeProjects.Schneekönigin);
                MusicianProfile profile = MusicianProfileSeedData.PerformerMusicianProfile;
                profile.SetProperty(nameof(MusicianProfile.Instrument), SectionSeedData.Alto);
                profile.SetProperty(nameof(MusicianProfile.Person), PersonTestSeedData.Performer);
                profile.SetProperty(nameof(MusicianProfile.Qualification), FakeSelectValueMappings.Amateur);
                participation.SetProperty(nameof(ProjectParticipation.MusicianProfile), profile);
                participation.SetProperty(nameof(ProjectParticipation.InvitationStatus), FakeSelectValueMappings.Invited);
                participation.SetProperty(nameof(ProjectParticipation.ParticipationStatusInternal), FakeSelectValueMappings.Candidate);
                participation.SetProperty(nameof(ProjectParticipation.ParticipationStatusInner), FakeSelectValueMappings.Acceptance);
                return participation;
            }
        }

        public static ProjectParticipation PerformerRockingXMasParticipation
        {
            get
            {
                ProjectParticipation participation = ProjectParticipationSeedData.PerformerRockingXMasParticipation;
                participation.SetProperty(nameof(ProjectParticipation.Project), ProjectSeedData.RockingXMas);
                return participation;
            }
        }

        public static ProjectParticipation StaffParticipation1
        {
            get
            {
                ProjectParticipation participation = ProjectParticipationSeedData.StaffParticipation1;
                participation.SetProperty(nameof(ProjectParticipation.Project), ProjectSeedData.RockingXMas);
                return participation;
            }
        }

        public static ProjectParticipation StaffParticipation2
        {
            get
            {
                ProjectParticipation participation = ProjectParticipationSeedData.StaffParticipation2;
                participation.SetProperty(nameof(ProjectParticipation.Project), ProjectSeedData.RockingXMas);
                return participation;
            }
        }

        public static ProjectParticipation AdminParticipation
        {
            get
            {
                ProjectParticipation participation = ProjectParticipationSeedData.AdminParticipation;
                participation.SetProperty(nameof(ProjectParticipation.Project), ProjectSeedData.RockingXMas);
                return participation;
            }
        }
    }
}
