using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

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
                    IsProfessional = MusicianProfileSeedData.PerformerMusicianProfile.IsProfessional,
                    SectionName = SectionSeedData.Alto1.Name
                };
            }
        }

        public static MusicianProfileDto StaffProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.StaffMusicianProfile1.IsProfessional,
                    SectionName = SectionSeedData.Tenor1.Name
                };
            }
        }

        public static MusicianProfileDto StaffProfile2
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.StaffMusicianProfile2.IsProfessional,
                    SectionName = SectionSeedData.Tenor2.Name
                };
            }
        }

        public static MusicianProfileDto AdminProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.AdminMusicianProfile1.IsProfessional,
                    SectionName = SectionSeedData.Soprano1.Name
                };
            }
        }
    }
}
