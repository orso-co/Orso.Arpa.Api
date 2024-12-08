using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class EducationSeedData
    {
        public static IList<Education> Educations =>
        [
            University,
        ];

        public static Education University
        {
            get
            {
                var command = new CreateEducation.Command("1990-1996",
                   "Hochschule für Musik und Darstellende Kunst Stuttgart",
                   SelectValueMappingSeedData.EducationTypeMappings[0].Id,
                   "Theater- und Orchestermanagement Master",
                   1,
                   MusicianProfileSeedData.PerformersHornMusicianProfile.Id);
                return new Education(Guid.Parse("a3541df8-8bd9-46e5-a61c-f73e30a2e2a1"), command);
            }
        }
    }
}
