using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.CurriculumVitaeReferences;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class CurriculumVitaeReferenceSeedData
    {
        public static IList<CurriculumVitaeReference> CurriculumVitaeReferences => new List<CurriculumVitaeReference>
        {
            Mozarteum,
        };

        public static CurriculumVitaeReference Mozarteum
        {
            get
            {
                var command = new Create.Command("2020",
                   "Mozarteum Salzburg",
                   Guid.Parse("674abb4f-89d1-4802-bfee-8eb0d61bed80"),
                   "Preis für außergewöhnliche Leistung bei der Interpretation der Werke von Mozart",
                   2,
                   Guid.Parse("056a27f0-cd88-4cd9-8729-ce2f23b8b0ef")); //PerformersDeactivatedTubaProfile
                return new CurriculumVitaeReference(Guid.Parse("ea0ad4a1-9351-457f-ab82-24e593e062a1"), command);
            }
        }
    }
}
