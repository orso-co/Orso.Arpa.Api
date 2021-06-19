using System;
using System.Collections.Generic;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class EducationDtoData
    {
        public static IList<EducationDto> Educations
        {
            get
            {
                return new List<EducationDto>
                {
                    University,
                };
            }
        }

        public static EducationDto University => new(
            Guid.Parse("d6b32609-0b3b-4ae6-a519-b936af0ad3bc"),
            "2021-2022",
            "Hochschule für Musik und Darstellende Kunst Frankfurt am Main",
            Guid.Parse("371ee51d-3612-4eb4-b169-25eae26c382f"),
            "Theater- und Orchestermanagement Master",
            2,
            "anonymous",
            FakeDateTime.UtcNow);
    }
}
