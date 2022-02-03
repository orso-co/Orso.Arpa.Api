using System;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ReducedMusicianProfileDtoData
    {
        public static ReducedMusicianProfileDto PerformerProfile
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Alto.Name,
                    Qualification = SelectValueSeedData.Amateur.Name,
                    Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                    DoublingInstrumentNames = string.Empty
                };
            }
        }

        public static ReducedMusicianProfileDto PerformerHornProfile
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Horn.Name,
                    Qualification = SelectValueSeedData.Student.Name,
                    Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                    DoublingInstrumentNames = SectionSeedData.WagnerTuba.Name
                };
            }
        }

        public static ReducedMusicianProfileDto PerformerDeactivatedTubaProfile
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Tuba.Name,
                    Id = Guid.Parse("056a27f0-cd88-4cd9-8729-ce2f23b8b0ef"),
                    Deactivation = new MusicianProfileDeactivationDto
                    {
                        CreatedAt = FakeDateTime.UtcNow,
                        CreatedBy = "anonymous",
                        DeactivationStart = new DateTime(2030, 1, 13),
                        Id = Guid.Parse("c3bed69d-f880-41e6-8075-ebea53caf816"),
                        Purpose = "Ich lerne zur Zeit Fagott und hab keine Zeit mehr, Tuba zu spielen."
                    },
                    DoublingInstrumentNames = string.Empty
                };
            }
        }

        public static ReducedMusicianProfileDto StaffProfile1
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Tenor.Name,
                    Id = Guid.Parse("f9d85f58-9156-4d5d-988b-3a3d0cb67205"),
                    DoublingInstrumentNames = string.Empty
                };
            }
        }

        public static ReducedMusicianProfileDto StaffProfile2
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Bass.Name,
                    Id = Guid.Parse("c2b727eb-16f7-440d-a003-aab073532bbf"),
                    DoublingInstrumentNames = string.Empty
                };
            }
        }

        public static ReducedMusicianProfileDto AdminProfile1
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Soprano.Name,
                    Id = Guid.Parse("7c215684-ee09-424f-9955-9b427494eaeb"),
                    DoublingInstrumentNames = string.Empty
                };
            }
        }

        public static ReducedMusicianProfileDto AdminProfile2
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Flute.Name,
                    Id = Guid.Parse("9f6f3cab-6b0d-463e-8d66-58b9c760d498"),
                    DoublingInstrumentNames = string.Empty
                };
            }
        }

        public static ReducedMusicianProfileDto WithoutRoleProfile
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Bass.Name,
                    Id = Guid.Parse("1a7a62f2-1ca0-4eed-9053-b59bc6db34d6"),
                    DoublingInstrumentNames = string.Empty
                };
            }
        }
    }
}
