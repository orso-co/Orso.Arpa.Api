using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.MusicianProfileApplication.Model
{
    public class ReducedMusicianProfileDto
    {
        public Guid Id { get; set; }
        [Translate(LocalizationKeys.SECTION)]
        public string InstrumentName { get; set; }
        [Translate(LocalizationKeys.SELECT_VALUE)]
        public string Qualification { get; set; }

        [Translate(LocalizationKeys.SECTION)]
        public List<string> DoublingInstrumentNames { get; set; } = new List<string>();
        public MusicianProfileDeactivationDto Deactivation { get; set; }
    }

    public class ReducedMusicianProfileDtoMappingProfile : Profile
    {
        public ReducedMusicianProfileDtoMappingProfile()
        {
            _ = CreateMap<MusicianProfile, ReducedMusicianProfileDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification == null ? string.Empty : src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument.Name))
                .ForMember(dest => dest.DoublingInstrumentNames, opt => opt.MapFrom(src => src.DoublingInstruments.Select(di => di.Section.Name)))
                .AfterMap<LocalizeAction<MusicianProfile, ReducedMusicianProfileDto>>();
        }
    }
}
