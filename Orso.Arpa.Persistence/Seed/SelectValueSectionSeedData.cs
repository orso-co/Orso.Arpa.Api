using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class SelectValueSectionSeedData
    {
        public static IList<SelectValueSection> SelectValueSections
        {
            get
            {
                return new List<SelectValueSection>
                {
                    HornSolo,
                    HornLow,
                    HornHigh,
                    HornCoach,
                    ClarinetSolo,
                    ClarinetTutti,
                    ClarinetCoach,
                    SopranoSolo,
                    SopranoHigh,
                    SopranoLow,
                    SopranoCoach
                };
            }
        }

        public static SelectValueSection HornSolo => new(Guid.Parse("4abea964-f83c-4973-a376-6e7782da6e7e"), SectionSeedData.Horn.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection HornHigh => new(Guid.Parse("b43fc897-ebcf-4d2a-8682-33b6337b5ab2"), SectionSeedData.Horn.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection HornLow => new(Guid.Parse("42525d3a-e158-44ee-88b5-1a4332a77862"), SectionSeedData.Horn.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection HornCoach => new(Guid.Parse("2e43c349-0a3b-4860-94fc-34e87a306845"), SectionSeedData.Horn.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection ClarinetSolo => new(Guid.Parse("c7b2bf38-3fb0-46a1-93c1-a41f3d865d96"), SectionSeedData.Clarinet.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection ClarinetTutti => new(Guid.Parse("1524b2d5-609c-41b2-bbd3-bba7cfa521f9"), SectionSeedData.Clarinet.Id, SelectValueSeedData.Tutti.Id);
        public static SelectValueSection ClarinetCoach => new(Guid.Parse("d3b924d1-68ad-429f-a6e4-fab48b251470"), SectionSeedData.Clarinet.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection SopranoSolo => new(Guid.Parse("5748698c-fc7f-437e-867c-d3c3dc4dcf4e"), SectionSeedData.Soprano.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection SopranoHigh => new(Guid.Parse("3ecfed41-1b06-4dca-b3e1-ed84459e2493"), SectionSeedData.Soprano.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection SopranoLow => new(Guid.Parse("a08ba21d-c850-4485-aabc-c42a1a016953"), SectionSeedData.Soprano.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection SopranoCoach => new(Guid.Parse("497d2236-48a4-46a2-90c5-ef6f7d13f6a8"), SectionSeedData.Soprano.Id, SelectValueSeedData.Coach.Id);

        // ToDo: Add additional seed data here
    }
}
