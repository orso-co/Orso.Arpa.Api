using System;
using System.Collections.Generic;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ProjectDtoData
    {
        public static IList<ProjectDto> ProjectsForPerformer
        {
            get
            {
                return new List<ProjectDto>
                {
                    RockingXMasForPerformer,
                    HoorayForHollywood
                };
            }
        }

        public static IList<ProjectDto> ProjectsForStaff
        {
            get
            {
                return new List<ProjectDto>
                {
                    RockingXMasForStaff,
                    HoorayForHollywood
                };
            }
        }

        public static ProjectDto RockingXMasForPerformer
        {
            get
            {
                ProjectDto dto = RockingXMasBase;
                dto.Urls.Add(UrlDtoData.OrsoWebsite);
                dto.Urls.Add(UrlDtoData.Google);
                return dto;
            }
        }

        public static ProjectDto RockingXMasForStaff
        {
            get
            {
                ProjectDto dto = RockingXMasBase;
                dto.Urls.Add(UrlDtoData.ArpaWebsite);
                dto.Urls.Add(UrlDtoData.OrsoWebsite);
                return dto;
            }
        }

        private static ProjectDto RockingXMasBase
        {
            get
            {
                return new ProjectDto
                {
                    Title = "Rocking X-mas Freiburg",
                    ShortTitle = "RockXmas",
                    Description = "Rocking around the christmas tree",
                    Number = "1005",
                    TypeId = SelectValueMappingSeedData.ProjectTypeMappings[0].Id,
                    GenreId = SelectValueMappingSeedData.ProjectGenreMappings[0].Id,
                    StartDate = new DateTime(2020, 12, 24),
                    EndDate = new DateTime(2020, 12, 26),
                    StateId = SelectValueMappingSeedData.ProjectStateMappings[0].Id,
                    ParentId = null,
                    IsCompleted = true,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                    CreatedAt = new DateTime(2021, 1, 1),
                };
            }
        }

        public static ProjectDto HoorayForHollywood
        {
            get
            {
                return new ProjectDto
                {
                    Title = "Hooray for Hollywood Freiburg",
                    ShortTitle = "Hollywood",
                    Description = "Winter is Coming",
                    Number = "1006",
                    TypeId = SelectValueMappingSeedData.ProjectTypeMappings[1].Id,
                    GenreId = SelectValueMappingSeedData.ProjectGenreMappings[1].Id,
                    StartDate = new DateTime(2020, 05, 05),
                    EndDate = new DateTime(2020, 06, 06),
                    Urls = new List<UrlDto> { UrlDtoData.GoogleDe },
                    StateId = SelectValueMappingSeedData.ProjectStateMappings[1].Id,
                    ParentId = null,
                    IsCompleted = false,

                    CreatedBy = "anonymous",
                    Id = Guid.Parse("8f8c500f-71f5-49be-92c8-150ac9e88219"),
                    CreatedAt = new DateTime(2021, 1, 1),
                };
            }
        }
    }
}
