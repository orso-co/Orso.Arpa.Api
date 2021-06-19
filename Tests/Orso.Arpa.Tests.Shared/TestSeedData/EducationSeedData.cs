using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Educations;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class EducationSeedData
    {
        public static IList<Education> Educations => new List<Education>
        {
            University,
        };

        public static Education University
        {
            get
            {
                var command = new Create.Command("2021-2022",
                   "Hochschule für Musik und Darstellende Kunst Frankfurt am Main",
                   Guid.Parse("371ee51d-3612-4eb4-b169-25eae26c382f"),
                   "Theater- und Orchestermanagement Master",
                   2,
                   Guid.Parse("056a27f0-cd88-4cd9-8729-ce2f23b8b0ef")); //PerformersDeactivatedTubaProfile
                return new Education(Guid.Parse("a3541df8-8bd9-46e5-a61c-f73e30a2e2a1"), command);
            }
        }
    }
}
