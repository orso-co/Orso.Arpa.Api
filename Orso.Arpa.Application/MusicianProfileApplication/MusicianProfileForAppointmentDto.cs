using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileForAppointmentApplication
{
    public class MusicianProfileForAppointmentDto
    {
        public string InstrumentName { get; set; }
        public string Qualification { get; set; }
    }

    public class MusicianProfileForAppointmentDtoMappingProfile : Profile
    {
        public MusicianProfileForAppointmentDtoMappingProfile()
        {
            CreateMap<MusicianProfile, MusicianProfileForAppointmentDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument.Name));
        }
    }
}
