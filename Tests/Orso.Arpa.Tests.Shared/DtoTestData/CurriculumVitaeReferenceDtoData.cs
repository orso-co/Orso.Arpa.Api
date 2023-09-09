using System;
using System.Collections.Generic;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class CurriculumVitaeReferenceDtoData
    {
        public static IList<CurriculumVitaeReferenceDto> CurriculumVitaeReferences
        {
            get
            {
                return new List<CurriculumVitaeReferenceDto>
                {
                    Mozarteum,
                };
            }
        }

        public static CurriculumVitaeReferenceDto Mozarteum
        {
            get
            {
                return new CurriculumVitaeReferenceDto
                {
                    TimeSpan = "1998-2000",
                    Institution = "Mozarteum Salzburg",
                    TypeId = SelectValueMappingSeedData.CurriculumVitaeReferenceTypeMappings[0].Id,
                    Description = "Preis für außergewöhnliche Leistung bei der Interpretation der Werke von Mozart",
                    SortOrder = 1,
                    Id = Guid.Parse("ea0ad4a1-9351-457f-ab82-24e593e062a1"),
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous"
                };
            }
        }
    }
}
