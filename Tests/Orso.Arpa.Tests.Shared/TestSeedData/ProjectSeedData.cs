using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Projects;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class ProjectSeedData
    {
        public static IList<Project> Projects
        {
            get
            {
                return new List<Project>
                {
                    RockingXMas
                };
            }
        }

        public static Project RockingXMas
        {
            get
            {
                return new Project(
                    Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                    new Create.Command
                    {
                        Title = "Rocking X-mas Freiburg",
                        Description = "Rocking around the christmas tree",
                        GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                        Number = 1005,
                        ParentId = null
                    });
            }
        }
    }
}
