using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Misc;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeProjects
    {
        public static Project RockingXMas
        {
            get
            {
                Project project = ProjectSeedData.RockingXMas;
                project.ProjectAppointments.Add(FakeProjectAppointments.RockingXMasRehearsal);
                project.SetProperty(nameof(Project.CreatedBy), "anonymous");
                project.SetProperty(nameof(Project.CreatedAt), DateTimeProvider.Instance.GetUtcNow());
                return project;
            }
        }
    }
}
