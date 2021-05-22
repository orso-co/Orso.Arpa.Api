using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MyMusicianProfileDtoData
    {
        public static IList<MyMusicianProfileDto> ProfilesForTromboneAndEuphoniumPlayer
        {
            get
            {
                return new List<MyMusicianProfileDto>
                {
                    Trombonist,
                    EuphoniumPlayer,
                };
            }
        }

        public static MyMusicianProfileDto Trombonist
        {
            get
            {
                return new MyMusicianProfileDto
                {
                    Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                    PersonId = PersonTestSeedData.TrombonistAndEuphoniumPlayer.Id,

                    IsMainProfile = true,
                    IsDeactivated = false,

                    LevelAssessmentPerformer = 1,
                    ProfilePreferencePerformer = 3,

                    BackgroundPerformer = "Trombonist background description",

                    InstrumentId = SectionSeedData.Trombone.Id,
                    InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,

                    //Todo collections
                };
            }
        }
        public static MyMusicianProfileDto EuphoniumPlayer
        {
            get
            {
                return new MyMusicianProfileDto
                {
                    Id = Guid.Parse("6cac7b0e-f95e-4287-b29c-6e32f78c7e70"),
                    PersonId = PersonTestSeedData.TrombonistAndEuphoniumPlayer.Id,

                    IsMainProfile = true,
                    IsDeactivated = false,

                    LevelAssessmentPerformer = 1,
                    ProfilePreferencePerformer = 3,

                    BackgroundPerformer = "Trombonist who plays Euphonium, too",

                    InstrumentId = SectionSeedData.Euphonium.Id,
                    InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                };
            }
        }
    }
}
