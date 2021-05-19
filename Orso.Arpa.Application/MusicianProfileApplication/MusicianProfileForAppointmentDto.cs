using System;
using AutoMapper;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileForAppointmentApplication
{
    public class MusicianProfileForAppointmentDto
    {
        public Guid InstrumentId { get; set; }
        public Guid? QualificationId { get; set; }
    }

    public class MusicianProfileForAppointmentDtoMappingProfile : Profile
    {
        public MusicianProfileForAppointmentDtoMappingProfile()
        {
            CreateMap<MusicianProfile, MusicianProfileForAppointmentDto>()
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId));

            CreateMap<MusicianProfileDto, MusicianProfileForAppointmentDto>()
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId));
        }
    }
}
