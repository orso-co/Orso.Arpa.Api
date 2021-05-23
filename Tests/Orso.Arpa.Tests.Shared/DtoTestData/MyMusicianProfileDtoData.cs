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

        public static MyMusicianProfileDto PerformersTromboneMusicianProfile
        {
            get
            {
                return new MyMusicianProfileDto
                {
                    Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    InstrumentId = SectionSeedData.Trombone.Id,
                    LevelAssessmentPerformer = 1,
                    InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[1].Id,
                    BackgroundPerformer = "Background Trombonist",
                    ProfilePreferencePerformer = 3,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = false,
                };
            }
        }

        public static MyMusicianProfileDto PerformersDeactivatedTubaProfile
        {
            get
            {
                return new MyMusicianProfileDto
                {
                    Id = Guid.Parse("056a27f0-cd88-4cd9-8729-ce2f23b8b0ef"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    InstrumentId = SectionSeedData.Tuba.Id,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = false,
                    IsDeactivated = true,
                };
            }
        }
    }
}
