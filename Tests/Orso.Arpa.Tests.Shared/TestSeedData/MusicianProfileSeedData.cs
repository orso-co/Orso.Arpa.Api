using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class MusicianProfileSeedData
    {
        public static IList<MusicianProfile> MusicianProfiles
        {
            get
            {
                return new List<MusicianProfile>
                {
                    PerformerMusicianProfile,
                    StaffMusicianProfile1,
                    StaffMusicianProfile2,
                    AdminMusicianProfile1,
                    AdminMusicianProfile2,
                    UserWithoutRoleMusicianProfile
                };
            }
        }

        public static MusicianProfile PerformerMusicianProfile
        {
            get
            {
                return new MusicianProfile(Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"), new Create.Command
                {
                    IsProfessional = false,
                    PersonId = PersonTestSeedData.Performer.Id,
                    SectionId = SectionSeedData.Alto1.Id
                });
            }
        }

        public static MusicianProfile StaffMusicianProfile1
        {
            get
            {
                return new MusicianProfile(Guid.Parse("f9d85f58-9156-4d5d-988b-3a3d0cb67205"), new Create.Command
                {
                    IsProfessional = true,
                    PersonId = PersonTestSeedData.Staff.Id,
                    SectionId = SectionSeedData.Tenor1.Id
                });
            }
        }

        public static MusicianProfile StaffMusicianProfile2
        {
            get
            {
                return new MusicianProfile(Guid.Parse("c2b727eb-16f7-440d-a003-aab073532bbf"), new Create.Command
                {
                    IsProfessional = false,
                    PersonId = PersonTestSeedData.Staff.Id,
                    SectionId = SectionSeedData.Tenor2.Id
                });
            }
        }

        public static MusicianProfile AdminMusicianProfile1
        {
            get
            {
                return new MusicianProfile(Guid.Parse("7c215684-ee09-424f-9955-9b427494eaeb"), new Create.Command
                {
                    IsProfessional = false,
                    PersonId = PersonSeedData.Admin.Id,
                    SectionId = SectionSeedData.Soprano1.Id
                });
            }
        }

        public static MusicianProfile AdminMusicianProfile2
        {
            get
            {
                return new MusicianProfile(Guid.Parse("9f6f3cab-6b0d-463e-8d66-58b9c760d498"), new Create.Command
                {
                    IsProfessional = false,
                    PersonId = PersonSeedData.Admin.Id,
                    SectionId = SectionSeedData.Soprano2.Id
                });
            }
        }

        public static MusicianProfile UserWithoutRoleMusicianProfile
        {
            get
            {
                return new MusicianProfile(Guid.Parse("1a7a62f2-1ca0-4eed-9053-b59bc6db34d6"), new Create.Command
                {
                    IsProfessional = true,
                    PersonId = PersonTestSeedData.UserWithoutRole.Id,
                    SectionId = SectionSeedData.Basso1.Id
                });
            }
        }
    }
}
