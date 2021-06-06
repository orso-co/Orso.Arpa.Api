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
                    InstrumentId = SectionSeedData.Alto.Id,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = true,
                    QualificationId = Guid.Parse("f036bca9-95d4-4526-b845-fff9208ab103")
                };
            }
        }

        public static MusicianProfileDto PerformersHornMusicianProfile
        {
            get
            {
                var dto = new MusicianProfileDto
                {
                    Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    InstrumentId = Guid.Parse("b9532add-efec-4510-831c-902c32ef7dbb"),
                    LevelAssessmentPerformer = 1,
                    InquiryStatusPerformerId = Guid.Parse("60c1a391-59b4-4cea-ba83-59e09f7512b6"),
                    BackgroundPerformer = "Background Trombonist",
                    ProfilePreferencePerformer = 3,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = false,
                    QualificationId = Guid.Parse("6304b935-633d-4bba-a90f-9bd864c867c6")
                };
                dto.DoublingInstruments.Add(new DoublingInstrumentDto
                {
                    AvailabilityId = Guid.Parse("d33ea034-0c5f-458d-bef5-26d2c12b6b03"),
                    Comment = "Wagner rocks",
                    LevelAssessmentPerformer = 3,
                    LevelAssessmentStaff = 2,
                    Id = Guid.Parse("d57c5706-f0aa-4e02-829c-e7823ed7a63d"),
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    InstrumentId = Guid.Parse("c42591db-4e41-413f-8b98-6607e2f12e39")
                });
                dto.PreferredPositionsStaffIds.Add(Guid.Parse("2e43c349-0a3b-4860-94fc-34e87a306845"));
                dto.PreferredPositionsPerformerIds.Add(Guid.Parse("b43fc897-ebcf-4d2a-8682-33b6337b5ab2"));
                dto.PreferredPartsStaff.Add(1);
                dto.PreferredPartsPerformer.Add(2);
                return dto;
            }
        }

        public static MusicianProfileDto PerformersDeactivatedTubaProfile
        {
            get
            {
                return new MusicianProfileDto
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

        public static MusicianProfileDto StaffProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    InstrumentId = SectionSeedData.Basso.Id,

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
                    InstrumentId = SectionSeedData.Tenor.Id,

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
                    InstrumentId = SectionSeedData.Soprano.Id,

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
                    InstrumentId = SectionSeedData.Basso.Id,

                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow,
                    IsMainProfile = false
                };
            }
        }
    }
}
