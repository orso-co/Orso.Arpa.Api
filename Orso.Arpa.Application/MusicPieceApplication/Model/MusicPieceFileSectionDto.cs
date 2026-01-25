using System;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceFileSectionDto
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }

        [Translate(LocalizationKeys.SECTION)]
        public string SectionName { get; set; }
    }

    public class MusicPieceFileSectionDtoMappingProfile : Profile
    {
        public MusicPieceFileSectionDtoMappingProfile()
        {
            _ = CreateMap<MusicPieceFileSection, MusicPieceFileSectionDto>()
                .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.Name))
                .AfterMap<LocalizeAction<MusicPieceFileSection, MusicPieceFileSectionDto>>();
        }
    }
}
