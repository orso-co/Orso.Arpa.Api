using System;
using System.Collections.Generic;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ProjectDtoData
    {
        public static IList<ProjectDto> ProjectsForStaff
        {
            get
            {
                return new List<ProjectDto>
                {
                    RockingXMasForStaff,
                    HoorayForHollywood,
                    Schneekönigin,
                    Chorwerkstatt,
                    ChorwerkstattFreiburg,
                    ChorwerkstattBerlin,
                    ChorwerkstattStuttgart
                };
            }
        }

        public static IList<ProjectDto> NotCompletedProjectsForStaff
        {
            get
            {
                return new List<ProjectDto>
                {
                    HoorayForHollywood,
                    Schneekönigin,
                    Chorwerkstatt,
                    ChorwerkstattBerlin,
                    ChorwerkstattStuttgart
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
                dto.Urls.Add(UrlDtoData.Google);
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
                    Code = "1005",
                    Type = SelectValueDtoData.Concert,
                    Genre = SelectValueDtoData.ClassicalMusic,
                    StartDate = new DateTime(2020, 12, 24),
                    EndDate = new DateTime(2020, 12, 26),
                    Status = ProjectStatus.Pending,
                    ParentId = null,
                    IsCompleted = true,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("a19d84f1-4ac1-49c3-abfe-527092b80b6d"),
                    CreatedAt = FakeDateTime.UtcNow,
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
                    Code = "1006",
                    Type = SelectValueDtoData.ConcertTour,
                    Genre = SelectValueDtoData.Crossover,
                    StartDate = new DateTime(2020, 05, 05),
                    EndDate = new DateTime(2020, 06, 06),
                    Urls = new List<UrlDto> { UrlDtoData.GoogleDe },
                    Status = ProjectStatus.Confirmed,
                    ParentId = null,
                    IsCompleted = false,

                    CreatedBy = "anonymous",
                    Id = Guid.Parse("8f8c500f-71f5-49be-92c8-150ac9e88219"),
                    CreatedAt = FakeDateTime.UtcNow,
                };
            }
        }

        public static ProjectDto Schneekönigin
        {
            get
            {
                return new ProjectDto
                {
                    Code = "1007",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Description = "Let it snow",
                    EndDate = new DateTime(2020, 12, 10),
                    Genre = SelectValueDtoData.ClassicalMusic,
                    Id = Guid.Parse("b781c54d-8115-4561-b01e-9836fa05175e"),
                    ShortTitle = "Schnee",
                    StartDate = new DateTime(2020, 12, 1),
                    Status = ProjectStatus.Pending,
                    Title = "Die Schneekönigin",
                    Type = SelectValueDtoData.Concert
                };
            }
        }

        public static ProjectDto Chorwerkstatt
        {
            get
            {
                return new ProjectDto
                {
                    Code = "1002",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Description = "Ein Blick hinter die Kulissen…",
                    EndDate = new DateTime(2020, 11, 30),
                    Genre = SelectValueDtoData.FilmMusic,
                    Id = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7"),
                    ShortTitle = "ChWeTour",
                    StartDate = new DateTime(2020, 9, 1),
                    Status = ProjectStatus.Confirmed,
                    Title = "Chorwerkstatt",
                    Type = SelectValueDtoData.ConcertTour
                };
            }
        }

        public static ProjectDto ChorwerkstattFreiburg
        {
            get
            {
                return new ProjectDto
                {
                    Code = "1003",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Description = "Ein Blick hinter die Kulissen…",
                    EndDate = new DateTime(2020, 10, 30),
                    Genre = SelectValueDtoData.FilmMusic,
                    Id = Guid.Parse("a9668e17-e6df-4b08-8db5-30c88f89d78c"),
                    ShortTitle = "ChWeFr",
                    StartDate = new DateTime(2020, 10, 10),
                    Status = ProjectStatus.Cancelled,
                    Title = "Chorwerkstatt Freiburg",
                    Type = SelectValueDtoData.Concert,
                    ParentId = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7")
                };
            }
        }

        public static ProjectDto ChorwerkstattBerlin
        {
            get
            {
                return new ProjectDto
                {
                    Code = "1004",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Description = "Ein Blick hinter die Kulissen…",
                    EndDate = new DateTime(2020, 11, 30),
                    Genre = SelectValueDtoData.FilmMusic,
                    Id = Guid.Parse("f1bea5ee-6569-4db1-9a46-c425a5571be2"),
                    ShortTitle = "ChWeB",
                    StartDate = new DateTime(2020, 11, 1),
                    Status = ProjectStatus.Confirmed,
                    Title = "Chorwerkstatt Berlin",
                    Type = SelectValueDtoData.Concert,
                    ParentId = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7")
                };
            }
        }

        public static ProjectDto ChorwerkstattStuttgart
        {
            get
            {
                return new ProjectDto
                {
                    Code = "1008",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Description = "Ein Blick hinter die Kulissen…",
                    EndDate = new DateTime(2020, 11, 20),
                    Genre = SelectValueDtoData.FilmMusic,
                    Id = Guid.Parse("483927b5-19de-4677-8af4-482c271ffae4"),
                    ShortTitle = "ChWeS",
                    StartDate = new DateTime(2020, 11, 10),
                    Status = ProjectStatus.Pending,
                    Title = "Chorwerkstatt Stuttgart",
                    Type = SelectValueDtoData.Concert,
                    ParentId = Guid.Parse("785f48b6-0f55-406f-8180-ec60501407d7"),
                    IsHiddenForPerformers = true,
                };
            }
        }
    }
}
