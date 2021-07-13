using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.MusicianProfiles;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

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
                    PerformersHornMusicianProfile,
                    PerformersDeactivatedTubaProfile,
                    StaffMusicianProfile1,
                    StaffMusicianProfile2,
                    AdminMusicianSopranoProfile,
                    AdminMusicianFluteProfile,
                    UserWithoutRoleMusicianProfile,
                };
            }
        }

        public static MusicianProfile PerformerMusicianProfile
        {
            get
            {
                var profile = new MusicianProfile(new Create.Command
                {
                    PersonId = PersonTestSeedData.Performer.Id,
                    InstrumentId = SectionSeedData.Alto.Id,
                }, true, Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"));
                profile.SetProperty(nameof(MusicianProfile.QualificationId), SelectValueMappingSeedData.MusicianProfileQualificationMappings[0].Id);
                profile.Documents.Add(new MusicianProfileDocument(
                    profile.Id,
                    SelectValueMappingSeedData.MusicianProfileDocumentsMappings[0].Id,
                    Guid.Parse("b1d10592-4106-46d8-8c78-eedc77c0e3bf")));
                return profile;
            }
        }

        public static MusicianProfile PerformersHornMusicianProfile
        {
            get
            {
                var command = new Create.Command
                {
                    PersonId = PersonTestSeedData.Performer.Id,
                    InstrumentId = SectionSeedData.Horn.Id,
                    LevelAssessmentInner = 1,
                    InquiryStatusInnerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusInnerMappings[1].Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[1].Id
                };
                command.PreferredPositionsTeamIds.Add(SelectValueSectionSeedData.HornCoach.Id);
                command.PreferredPositionsInnerIds.Add(SelectValueSectionSeedData.HornHigh.Id);
                command.PreferredPartsTeam.Add(1);
                command.PreferredPartsInner.Add(2);
                var profile = new MusicianProfile(command, false, Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"));
                profile.SetProperty(nameof(MusicianProfile.BackgroundInner), "Background Trombonist");
                profile.SetProperty(nameof(MusicianProfile.ProfilePreferenceInner), (byte)3);
                profile.DoublingInstruments.Add(new MusicianProfileSection(Guid.Parse("d57c5706-f0aa-4e02-829c-e7823ed7a63d"), new Domain.Logic.MusicianProfileSections.Create.Command
                {
                    AvailabilityId = SelectValueMappingSeedData.MusicianProfileSectionInstrumentAvailabilityMappings[0].Id,
                    Comment = "Wagner rocks",
                    InstrumentId = SectionSeedData.WagnerTuba.Id,
                    LevelAssessmentInner = 3,
                    LevelAssessmentTeam = 2,
                    MusicianProfileId = profile.Id
                }));
                return profile;
            }
        }

        public static MusicianProfile PerformersDeactivatedTubaProfile
        {
            get
            {
                var profile = new MusicianProfile(new Create.Command
                {
                    PersonId = Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    InstrumentId = SectionSeedData.Tuba.Id,
                }, false, Guid.Parse("056a27f0-cd88-4cd9-8729-ce2f23b8b0ef"));
                profile.SetProperty(nameof(MusicianProfile.IsDeactivated), true);
                return profile;
            }
        }
        public static MusicianProfile StaffMusicianProfile1
        {
            get
            {
                return new MusicianProfile(new Create.Command
                {
                    PersonId = PersonTestSeedData.Staff.Id,
                    InstrumentId = SectionSeedData.Tenor.Id
                }, true, Guid.Parse("f9d85f58-9156-4d5d-988b-3a3d0cb67205"));
            }
        }

        public static MusicianProfile StaffMusicianProfile2
        {
            get
            {
                return new MusicianProfile(new Create.Command
                {
                    PersonId = PersonTestSeedData.Staff.Id,
                    InstrumentId = SectionSeedData.Basso.Id
                }, true, Guid.Parse("c2b727eb-16f7-440d-a003-aab073532bbf"));
            }
        }

        public static MusicianProfile AdminMusicianSopranoProfile
        {
            get
            {
                return new MusicianProfile(new Create.Command
                {
                    PersonId = PersonSeedData.Admin.Id,
                    InstrumentId = SectionSeedData.Soprano.Id
                }, true, Guid.Parse("7c215684-ee09-424f-9955-9b427494eaeb"));
            }
        }

        public static MusicianProfile AdminMusicianFluteProfile
        {
            get
            {
                return new MusicianProfile(new Create.Command
                {
                    PersonId = PersonSeedData.Admin.Id,
                    InstrumentId = SectionSeedData.Flute.Id
                }, false, Guid.Parse("9f6f3cab-6b0d-463e-8d66-58b9c760d498"));
            }
        }

        public static MusicianProfile UserWithoutRoleMusicianProfile
        {
            get
            {
                return new MusicianProfile(new Create.Command
                {
                    PersonId = PersonTestSeedData.UserWithoutRole.Id,
                    InstrumentId = SectionSeedData.Basso.Id
                }, true, Guid.Parse("1a7a62f2-1ca0-4eed-9053-b59bc6db34d6"));
            }
        }
    }
}
