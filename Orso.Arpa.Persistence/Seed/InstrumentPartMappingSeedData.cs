using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class InstrumentPartMappingSeedData
    {
        public static IList<InstrumentPart> InstrumentPartMappings
        {
            get
            {
                return new List<InstrumentPart>
                {
                    new InstrumentPart(Guid.Parse("4abea964-f83c-4973-a376-6e7782da6e7e"), SectionSeedData.Euphonium.Id, InstrumentPartSeedData.Part1.Id),
                };
            }
        }
    }
}
