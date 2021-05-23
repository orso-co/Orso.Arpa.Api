using System;
using System.Collections.Generic;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MusicianProfileDtoData
    {
        public static IList<MusicianProfileDto> ProfilesForTromboneAndEuphoniumPlayer
        {
            get
            {
                return new List<MusicianProfileDto>
                {
                    Trombonist,
                    EuphoniumPlayer,
                };
            }
        }

        public static MusicianProfileDto PerformerProfile
        {
            get
            {
                return new MusicianProfileDto
                {
                    Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),

                    InstrumentId = SectionSeedData.Alto1.Id,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = true
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
        public static MusicianProfileDto Trombonist
        {
            get
            {
                return new MusicianProfileDto
                {
                    Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                    PersonId = PersonTestSeedData.TrombonistAndEuphoniumPlayer.Id,

                    IsMainProfile = true,
                    IsDeactivated = false,

                    LevelAssessmentPerformer = 1,
                    LevelAssessmentStaff = 2,
                    ProfilePreferencePerformer = 3,
                    ProfilePreferenceStaff = 4,

                    BackgroundPerformer = "Trombonist background description",
                    BackgroundStaff = "Trombonist internal-background description",
                    SalaryComment = "Salary only via PayPal, other payments not accepted",

                    InstrumentId = SectionSeedData.Trombone.Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
                    SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
                    InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
                    InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow

                    //Todo collections
                };
            }
        }
        public static MusicianProfileDto EuphoniumPlayer
        {
            get
            {
                return new MusicianProfileDto
                {
                    Id = Guid.Parse("6cac7b0e-f95e-4287-b29c-6e32f78c7e70"),
                    PersonId = PersonTestSeedData.TrombonistAndEuphoniumPlayer.Id,

                    IsMainProfile = true,
                    IsDeactivated = false,

                    LevelAssessmentPerformer = 1,
                    LevelAssessmentStaff = 2,
                    ProfilePreferencePerformer = 3,
                    ProfilePreferenceStaff = 4,

                    BackgroundPerformer = "Trombonist who plays Euphonium, too",
                    BackgroundStaff = "Excellent guy",
                    SalaryComment = "PayPal only",

                    InstrumentId = SectionSeedData.Euphonium.Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
                    SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
                    InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
                    InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                };
            }
        }

        public static MusicianProfileDto TrumpetPlayer
        {
            get
            {
                return new MusicianProfileDto
                {
                    Id = Guid.Parse("6cac7b0e-f95e-4287-b29c-6e32f78c7e70"),
                    PersonId = PersonTestSeedData.Performer.Id,

                    InstrumentId = SectionSeedData.Trumpet.Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                };
            }
        }

    }
}
