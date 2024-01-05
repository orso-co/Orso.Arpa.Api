using System;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomSectionDto
    {
        [Translate(LocalizationKeys.SECTION)]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public int? Quantity { get; set; }

        [Translate(LocalizationKeys.ROOM_SECTION)]
        public string Description  { get; set; }
    }

    public class RoomSectionDtoMappingProfile : Profile
    {
        public RoomSectionDtoMappingProfile()
        {
            CreateMap<RoomSection, RoomSectionDto>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Section.Name))
                .AfterMap<LocalizeAction<RoomSection, RoomSectionDto>>();
        }
    }
}