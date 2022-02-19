using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class ReducedMusicianProfileDto
    {
        public Guid Id { get; set; }
        [Translate]
        public string InstrumentName { get; set; }
        [Translate]
        public string Qualification { get; set; }

        [Translate]
        public List<string> DoublingInstrumentNames { get; set; } = new List<string>();
        public MusicianProfileDeactivationDto Deactivation { get; set; }
    }

    public class ReducedMusicianProfileDtoMappingProfile : Profile
    {
        public ReducedMusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, ReducedMusicianProfileDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification == null ? string.Empty : src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument.Name))
                .ForMember(dest => dest.DoublingInstrumentNames, opt => opt.MapFrom(src => src.DoublingInstruments.Select(di => di.Section.Name)));
        }
    }
}
