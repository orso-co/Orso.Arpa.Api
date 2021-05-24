using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class InstrumentPartSeedData
    {
        public static IList<InstrumentPart> InstrumentParts
        {
            get
            {
                return new List<InstrumentPart>
                {
                    Part1,
                };
            }
        }

        /// <summary>
        /// Todo: Hier müssen die Seeddaten aller Instrument Parts eingefügt werden
        /// </summary>
        public static InstrumentPart Part1 => new(Guid.Parse("be07fb21-e622-4b7e-b6aa-4c4b61988b4b"), Guid.NewGuid(), Guid.NewGuid());
    }
}
