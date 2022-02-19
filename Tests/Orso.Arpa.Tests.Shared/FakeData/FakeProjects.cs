using Orso.Arpa.Domain.Entities;
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
                project.SetProperty(nameof(Project.CreatedAt), FakeDateTime.UtcNow);
                project.Urls.Add(FakeUrls.ArpaWebsite);
                project.Urls.Add(FakeUrls.OrsoWebsite);
                project.Urls.Add(FakeUrls.Google);
                project.SetProperty(nameof(Project.Type), FakeSelectValueMappings.Concert);
                project.SetProperty(nameof(Project.Genre), FakeSelectValueMappings.ClassicalMusic);
                project.SetProperty(nameof(Project.State), FakeSelectValueMappings.Pending);
                return project;
            }
        }

        public static Project Schneekönigin
        {
            get
            {
                Project project = ProjectSeedData.Schneekönigin;
                project.SetProperty(nameof(Project.CreatedBy), "anonymous");
                project.SetProperty(nameof(Project.CreatedAt), FakeDateTime.UtcNow);
                return project;
            }
        }
    }
}
