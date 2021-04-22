using System;
using System.Collections.Generic;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ProjectDtoData
    {
        public static IList<ProjectDto> Projects
        {
            get
            {
                return new List<ProjectDto>
                {
                    RockingXMas,
                    HoorayForHollywood
                };
            }
        }

        public static ProjectDto RockingXMas
        {
            get
            {
                return new ProjectDto
                {
                    CreatedBy = "anonymous",
                    Description = "Rocking around the christmas tree",
                    GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                    Number = 1005,
                    Title = "Rocking X-mas Freiburg",
                    Id = Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                    CreatedAt = new DateTime(2021, 1, 1)
                };
            }
        }

        public static ProjectDto HoorayForHollywood
        {
            get
            {
                return new ProjectDto
                {
                    CreatedBy = "anonymous",
                    Description = "Winter is Coming",
                    GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                    Number = 1006,
                    Title = "Hooray for Hollywood Freiburg",
                    Id = Guid.Parse("8f8c500f-71f5-49be-92c8-150ac9e88219"),
                    CreatedAt = new DateTime(2021, 1, 1)
                };
            }
        }
    }
}
