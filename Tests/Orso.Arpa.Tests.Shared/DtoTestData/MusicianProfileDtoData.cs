using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Persistence.Seed;

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
                    SectionName = SectionSeedData.Alto1.Name,
                    Qualification = SelectValueSeedData.Amateur.Name
                };
            }
        }

        public static MusicianProfileDto StaffProfile1
        {
            get
            {
                return new MusicianProfileDto
                {
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
                    SectionName = SectionSeedData.Soprano1.Name
                };
            }
        }

        public static MusicianProfileDto WithoutRoleProfile
        {
            get
            {
                return new MusicianProfileDto
                {
                    SectionName = SectionSeedData.Bass1.Name
                };
            }
        }
    }
}
