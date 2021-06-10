using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class ReducedMusicianProfileDto
    {
        public Guid Id { get; set; }
        public string InstrumentName { get; set; }
        public string Qualification { get; set; }
    }

    public class ReducedMusicianProfileDtoMappingProfile : Profile
    {
        public ReducedMusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, ReducedMusicianProfileDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument.Name));
        }
    }
}
