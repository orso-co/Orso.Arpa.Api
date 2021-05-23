using System;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MyMusicianProfileDtoData
    {
        public static MyMusicianProfileDto PerformerProfile
        {
            get
            {
                return new MyMusicianProfileDto
                {
                    Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    InstrumentId = SectionSeedData.Alto1.Id,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = true
                };
            }
        }
    }
}
