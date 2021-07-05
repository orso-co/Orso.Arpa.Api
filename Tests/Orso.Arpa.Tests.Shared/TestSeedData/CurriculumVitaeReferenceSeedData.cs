using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.CurriculumVitaeReferences;
using Orso.Arpa.Persistence.Seed;

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
                var command = new Create.Command("1998-2000",
                   "Mozarteum Salzburg",
                   SelectValueMappingSeedData.CurriculumVitaeReferenceTypeMappings[0].Id,
                   "Preis für außergewöhnliche Leistung bei der Interpretation der Werke von Mozart",
                   1,
                   MusicianProfileSeedData.PerformersHornMusicianProfile.Id);
                return new CurriculumVitaeReference(Guid.Parse("ea0ad4a1-9351-457f-ab82-24e593e062a1"), command);
            }
        }
    }
}
