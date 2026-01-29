using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;
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
        public List<string> DoublingInstrumentNames { get; set; } = [];
        public MusicianProfileDeactivationDto Deactivation { get; set; }

        [Translate(LocalizationKeys.SELECT_VALUE)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public List<string> PreferredPositionsTeam { get; set; }= [];

        /// <summary>
        /// Person info for display purposes (name, avatar)
        /// </summary>
        public PersonDto Person { get; set; }
    }

    public class ReducedMusicianProfileDtoMappingProfile : Profile
    {
        public ReducedMusicianProfileDtoMappingProfile()
        {
            _ = CreateMap<MusicianProfile, ReducedMusicianProfileDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification == null ? string.Empty : src.Qualification.SelectValue.Name))
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument.Name))
                .ForMember(dest => dest.DoublingInstrumentNames, opt => opt.MapFrom(src => src.DoublingInstruments.Select(di => di.Section.Name)))
                .ForMember(dest => dest.PreferredPositionsTeam, opt => opt.MapFrom(src => src.PreferredPositionsTeam.Select(di => di.SelectValueSection.SelectValue.Name)))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
                .AfterMap<RoleBasedSetNullAction<MusicianProfile, ReducedMusicianProfileDto>>()
                .AfterMap<LocalizeAction<MusicianProfile, ReducedMusicianProfileDto>>();
        }
    }
}
