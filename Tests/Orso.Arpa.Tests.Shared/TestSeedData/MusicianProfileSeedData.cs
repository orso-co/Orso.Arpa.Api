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
                    StaffMusicianProfile1,
                    StaffMusicianProfile2,
                    AdminMusicianProfile1,
                    AdminMusicianProfile2,
                    UserWithoutRoleMusicianProfile,
                    Trombonist,
                };
            }
        }

        public static MusicianProfile PerformerMusicianProfile
        {
            get
            {
                return new MusicianProfile(Guid.Parse("9a609084-a5b6-485f-8960-724a8b470b13"), new Create.Command
                {
                    PersonId = PersonTestSeedData.Performer.Id,
                    InstrumentId = SectionSeedData.Alto1.Id,
                });
            }
        }

        public static MusicianProfile StaffMusicianProfile1
        {
            get
            {
                return new MusicianProfile(Guid.Parse("f9d85f58-9156-4d5d-988b-3a3d0cb67205"), new Create.Command
                {
                    PersonId = PersonTestSeedData.Staff.Id,
                    InstrumentId = SectionSeedData.Tenor1.Id
                });
            }
        }

        public static MusicianProfile StaffMusicianProfile2
        {
            get
            {
                return new MusicianProfile(Guid.Parse("c2b727eb-16f7-440d-a003-aab073532bbf"), new Create.Command
                {
                    PersonId = PersonTestSeedData.Staff.Id,
                    InstrumentId = SectionSeedData.Tenor2.Id
                });
            }
        }

        public static MusicianProfile AdminMusicianProfile1
        {
            get
            {
                return new MusicianProfile(Guid.Parse("7c215684-ee09-424f-9955-9b427494eaeb"), new Create.Command
                {
                    PersonId = PersonSeedData.Admin.Id,
                    InstrumentId = SectionSeedData.Soprano1.Id
                });
            }
        }

        public static MusicianProfile AdminMusicianProfile2
        {
            get
            {
                return new MusicianProfile(Guid.Parse("9f6f3cab-6b0d-463e-8d66-58b9c760d498"), new Create.Command
                {
                    PersonId = PersonSeedData.Admin.Id,
                    InstrumentId = SectionSeedData.Soprano2.Id
                });
            }
        }

        public static MusicianProfile UserWithoutRoleMusicianProfile
        {
            get
            {
                return new MusicianProfile(Guid.Parse("1a7a62f2-1ca0-4eed-9053-b59bc6db34d6"), new Create.Command
                {
                    PersonId = PersonTestSeedData.UserWithoutRole.Id,
                    InstrumentId = SectionSeedData.Bass1.Id
                });
            }
        }

        public static MusicianProfile Trombonist
        {
            get
            {
                var muPro = new MusicianProfile(Guid.Parse("e2ef2e6c-035e-4fff-9293-a6a7b67524a9"), new Create.Command
                {
                    LevelAssessmentPerformer = 1,
                    LevelAssessmentStaff = 2,

                    PersonId = PersonTestSeedData.TrombonistAndEuphoniumPlayer.Id,
                    InstrumentId = SectionSeedData.Trombone.Id,
                    QualificationId = SelectValueMappingSeedData.MusicianProfileQualificationMappings[2].Id,
                    InquiryStatusPerformerId = SelectValueMappingSeedData.MusicianProfileInquiryStatusPerformerMappings[0].Id,
                    InquiryStatusStaffId = SelectValueMappingSeedData.MusicianProfileInquiryStatusStaffMappings[2].Id,
                });

                muPro.SetProperty(nameof(MusicianProfile.IsMainProfile), true);
                muPro.SetProperty(nameof(MusicianProfile.IsDeactivated), false);
                muPro.SetProperty(nameof(MusicianProfile.ProfilePreferencePerformer), (byte)3);
                muPro.SetProperty(nameof(MusicianProfile.ProfilePreferenceStaff), (byte)4);

                muPro.SetProperty(nameof(MusicianProfile.BackgroundPerformer), "Trombonist background description");
                muPro.SetProperty(nameof(MusicianProfile.BackgroundStaff), "Trombonist internal-background description");
                muPro.SetProperty(nameof(MusicianProfile.SalaryComment), "Salary only via PayPal, other payments not accepted");

                muPro.SetProperty(nameof(MusicianProfile.SalaryId), SelectValueMappingSeedData.MusicianProfileSalaryMappings[2].Id);

                //ToDo Collections

                return muPro;
            }
        }
    }
}
