using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeProjectParticipations
    {
        public static ProjectParticipation OrsianerProjectParticipation
        {
            get
            {
                ProjectParticipation participation = ProjectParticipationSeedData.OrsianerParticipation;
                participation.SetProperty(nameof(ProjectParticipation.Project), FakeProjects.RockingXMas);
                return participation;
            }
        }
    }
}
