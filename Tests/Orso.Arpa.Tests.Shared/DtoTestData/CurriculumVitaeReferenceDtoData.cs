using System;
using System.Collections.Generic;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
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

        public static CurriculumVitaeReferenceDto Mozarteum => new(
            Guid.Parse("da2c71a4-3c9d-4532-b845-edbe81540b60"),
            "2020",
            "Mozarteum Salzburg",
            Guid.Parse("674abb4f-89d1-4802-bfee-8eb0d61bed80"),
            "Preis für außergewöhnliche Leistung bei der Interpretation der Werke von Mozart",
            2,
            "anonymous",
            FakeDateTime.UtcNow);
    }
}
