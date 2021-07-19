using System;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class ReducedMusicianProfileDto
    {
        public Guid Id { get; set; }
        public string InstrumentName { get; set; }
        public string Qualification { get; set; }

        public string DoublingInstrumentNames { get; set; }
        public MusicianProfileDeactivationDto Deactivation { get; set; }
    }

    public class ReducedMusicianProfileDtoMappingProfile : Profile
    {
        public ReducedMusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, ReducedMusicianProfileDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument.Name))
                .ForMember(dest => dest.DoublingInstrumentNames, opt => opt.MapFrom(src => string.Join(", ", src.DoublingInstruments.Select(di => di.Section.Name))));
        }
    }
}
