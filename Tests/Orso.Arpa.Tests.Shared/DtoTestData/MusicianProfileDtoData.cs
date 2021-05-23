using System;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MusicianProfileDtoData
    {
        public static MusicianProfileDto PerformerProfile
        {
            get
            {
                return new MusicianProfileDto
                {
                    Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    InstrumentId = SectionSeedData.Alto1.Id,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = true,
                    QualificationId = Guid.Parse("f036bca9-95d4-4526-b845-fff9208ab103")
                };
            }
        }

        public static MusicianProfileDto StaffProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    InstrumentId = SectionSeedData.Tenor1.Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    IsMainProfile = true
                };
            }
        }

        public static MusicianProfileDto StaffProfile2
        {
            get
            {
                return new MusicianProfileDto
                {
                    InstrumentId = SectionSeedData.Tenor2.Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    IsMainProfile = false
                };
            }
        }

        public static MusicianProfileDto AdminProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    InstrumentId = SectionSeedData.Soprano1.Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    IsMainProfile = true
                };
            }
        }

        public static MusicianProfileDto WithoutRoleProfile
        {
            get
            {
                return new MusicianProfileDto
                {
                    InstrumentId = SectionSeedData.Bass1.Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    IsMainProfile = false
                };
            }
        }
    }
}
