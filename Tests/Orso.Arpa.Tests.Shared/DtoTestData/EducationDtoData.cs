using System;
using System.Collections.Generic;
using Orso.Arpa.Application.EducationApplication.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class EducationDtoData
    {
        public static IList<EducationDto> Educations
        {
            get
            {
                return
                [
                    University,
                ];
            }
        }

        public static EducationDto University
        {
            get
            {
                return new EducationDto
                {
                    TimeSpan = "1990-1996",
                    Institution = "Hochschule für Musik und Darstellende Kunst Stuttgart",
                    TypeId = SelectValueMappingSeedData.EducationTypeMappings[0].Id,
                    Description = "Theater- und Orchestermanagement Master",
                    SortOrder = 1,
                    Id = Guid.Parse("a3541df8-8bd9-46e5-a61c-f73e30a2e2a1"),
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Type = SelectValueDtoData.PrivateLesson
                };
            }
        }
    }
}
