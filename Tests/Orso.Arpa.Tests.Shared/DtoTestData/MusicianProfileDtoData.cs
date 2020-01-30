using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MusicianProfileDtoData
    {
        public static MusicianProfileDto OrsianerProfile
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.OrsianerMusicianProfile.IsProfessional,
                    SectionName = SectionSeedData.Alto1.Name
                };
            }
        }

        public static MusicianProfileDto OrsonautProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.OrsonautMusicianProfile1.IsProfessional,
                    SectionName = SectionSeedData.Tenor1.Name
                };
            }
        }

        public static MusicianProfileDto OrsonautProfile2
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.OrsonautMusicianProfile2.IsProfessional,
                    SectionName = SectionSeedData.Tenor2.Name
                };
            }
        }

        public static MusicianProfileDto OrsoadminProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
                    IsProfessional = MusicianProfileSeedData.OrsoadminMusicianProfile1.IsProfessional,
                    SectionName = SectionSeedData.Soprano1.Name
                };
            }
        }
    }
}
