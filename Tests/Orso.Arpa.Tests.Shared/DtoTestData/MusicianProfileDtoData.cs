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

                    LevelAssessmentPerformer = 1,
                    LevelAssessmentStaff = 2,
                    ProfilePreferencePerformer = 3,
                    ProfilePreferenceStaff = 4,
                    IsMainProfile = true,
                    Background = "Trombonist background description",
                    ExperienceLevel = 5,
                    SalaryComment = "Salary only via PayPal, other payments not accepted",

                    InstrumentId = SectionSeedData.Trombone.Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
                    SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
                    InquiryStatusPerfomerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
                    InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
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

                    LevelAssessmentPerformer = 1,
                    LevelAssessmentStaff = 2,
                    ProfilePreferencePerformer = 3,
                    ProfilePreferenceStaff = 4,
                    IsMainProfile = true,
                    Background = "Trombonist who plays Euphonium, too",
                    ExperienceLevel = 5,
                    SalaryComment = "PayPal only",

                    InstrumentId = SectionSeedData.Euphonium.Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
                    SalaryId = SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id,
                    InquiryStatusPerfomerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
                    InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                };
            }
        }
    }
}
