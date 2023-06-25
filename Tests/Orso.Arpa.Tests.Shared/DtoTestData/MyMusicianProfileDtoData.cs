using System;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MyMusicianProfileDtoData
    {
        public static MyMusicianProfileDto PerformerProfile
        {
            get
            {
                var dto = new MyMusicianProfileDto
                {
                    Id = Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    Instrument = SectionDtoData.Alto,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = true
                };
                dto.Documents.Add(Guid.Parse("f9cc5445-8a6e-480b-bffb-410089f55896"));
                dto.RegionPreferencesRehearsal.Add(new RegionPreferenceDto
                {
                    Id = Guid.Parse("0f3de639-a287-4246-b939-24780877030e"),
                    Comment = "Loving Freiburg so much...",
                    Rating = 5,
                    Region = RegionDtoData.Freiburg,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                });
                return dto;
            }
        }

        public static MyMusicianProfileDto PerformersHornMusicianProfile
        {
            get
            {
                var dto = new MyMusicianProfileDto
                {
                    Id = Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"),
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    Instrument = SectionDtoData.Horn,
                    LevelAssessmentInner = 1,
                    InquiryStatusInner = MusicianProfileInquiryStatus.EmergencyOnly,
                    BackgroundInner = "Background Trombonist",
                    ProfilePreferenceInner = 3,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = false,
                };
                dto.DoublingInstruments.Add(new MyDoublingInstrumentDto
                {
                    AvailabilityId = Guid.Parse("d33ea034-0c5f-458d-bef5-26d2c12b6b03"),
                    Comment = "Wagner rocks",
                    LevelAssessmentInner = 3,
                    Id = Guid.Parse("d57c5706-f0aa-4e02-829c-e7823ed7a63d"),
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    InstrumentId = Guid.Parse("c42591db-4e41-413f-8b98-6607e2f12e39")
                });
                dto.PreferredPositionsInnerIds.Add(Guid.Parse("b43fc897-ebcf-4d2a-8682-33b6337b5ab2"));
                dto.PreferredPartsInner.Add(2);
                dto.Educations.Add(EducationDtoData.University);
                dto.CurriculumVitaeReferences.Add(CurriculumVitaeReferenceDtoData.Mozarteum);
                return dto;
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
                    Instrument = SectionDtoData.Tuba,
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    IsMainProfile = false,
                    Deactivation = new MusicianProfileDeactivationDto
                    {
                        Id = Guid.Parse("c3bed69d-f880-41e6-8075-ebea53caf816"),
                        DeactivationStart = FakeDateTime.UtcNow.AddDays(-20),
                        CreatedAt = FakeDateTime.UtcNow,
                        CreatedBy = "anonymous",
                        Purpose = "Ich lerne zur Zeit Fagott und hab keine Zeit mehr, Tuba zu spielen."
                    }
                };
            }
        }
    }
}
