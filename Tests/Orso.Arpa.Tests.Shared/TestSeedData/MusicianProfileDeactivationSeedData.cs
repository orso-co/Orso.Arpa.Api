using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfileDeactivations;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class MusicianProfileDeactivationSeedData
    {
        public static IList<MusicianProfileDeactivation> MusicianProfileDeactivations
        {
            get
            {
                return new List<MusicianProfileDeactivation>
                {
                    PerformerTubaMusicianProfileDeactivation
                };
            }
        }

        public static MusicianProfileDeactivation PerformerTubaMusicianProfileDeactivation
        {
            get
            {
                return new MusicianProfileDeactivation(Guid.Parse("c3bed69d-f880-41e6-8075-ebea53caf816"), new Create.Command
                {
                    DeactivationStart = FakeDateTime.UtcNow.AddDays(-20),
                    MusicianProfileId = MusicianProfileSeedData.PerformersDeactivatedTubaProfile.Id,
                    Purpose = "Ich lerne zur Zeit Fagott und hab keine Zeit mehr, Tuba zu spielen."
                });
            }
        }
    }
}
