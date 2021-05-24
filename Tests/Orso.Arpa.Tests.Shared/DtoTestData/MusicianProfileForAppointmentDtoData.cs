using Orso.Arpa.Application.MusicianProfileForAppointmentApplication;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class MusicianProfileForAppointmentDtoData
    {
        public static MusicianProfileForAppointmentDto PerformerProfile
        {
            get
            {
                return new MusicianProfileForAppointmentDto
                {
                    InstrumentName = SectionSeedData.Alto1.Name,
                    Qualification = SelectValueSeedData.Amateur.Name
                };
            }
        }

        public static MusicianProfileForAppointmentDto StaffProfile1
        {
            get
            {
                return new MusicianProfileForAppointmentDto
                {
                    InstrumentName = SectionSeedData.Tenor1.Name
                };
            }
        }

        public static MusicianProfileForAppointmentDto StaffProfile2
        {
            get
            {
                return new MusicianProfileForAppointmentDto
                {
                    InstrumentName = SectionSeedData.Tenor2.Name
                };
            }
        }

        public static MusicianProfileForAppointmentDto AdminProfile1
        {
            get
            {
                return new MusicianProfileForAppointmentDto
                {
                    InstrumentName = SectionSeedData.Soprano1.Name
                };
            }
        }

        public static MusicianProfileForAppointmentDto WithoutRoleProfile
        {
            get
            {
                return new MusicianProfileForAppointmentDto
                {
                    InstrumentName = SectionSeedData.Bass1.Name
                };
            }
        }
    }
}
