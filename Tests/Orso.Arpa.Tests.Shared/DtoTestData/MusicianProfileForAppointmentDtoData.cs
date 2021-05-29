using System;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MusicianProfileForAppointmentDtoData
    {
        public static ReducedMusicianProfileDto PerformerProfile
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Alto1.Name,
                    Qualification = SelectValueSeedData.Amateur.Name,
                    Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13")
                };
            }
        }

        public static ReducedMusicianProfileDto StaffProfile1
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Tenor1.Name,
                    Id = Guid.Parse("f9d85f58-9156-4d5d-988b-3a3d0cb67205")
                };
            }
        }

        public static ReducedMusicianProfileDto StaffProfile2
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Tenor2.Name,
                    Id = Guid.Parse("c2b727eb-16f7-440d-a003-aab073532bbf")
                };
            }
        }

        public static ReducedMusicianProfileDto AdminProfile1
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Soprano1.Name,
                    Id = Guid.Parse("7c215684-ee09-424f-9955-9b427494eaeb")
                };
            }
        }

        public static ReducedMusicianProfileDto WithoutRoleProfile
        {
            get
            {
                return new ReducedMusicianProfileDto
                {
                    InstrumentName = SectionSeedData.Bass1.Name,
                    Id = Guid.Parse("1a7a62f2-1ca0-4eed-9053-b59bc6db34d6")
                };
            }
        }
    }
}
