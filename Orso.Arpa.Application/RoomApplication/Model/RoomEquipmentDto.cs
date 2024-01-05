using System;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomEquipmentDto
    {
        public Guid Id { get; set; }
        
        [Translate(LocalizationKeys.SELECT_VALUE)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [Translate(LocalizationKeys.ROOM_EQUIPMENT)]
        public string Description  { get; set; }
    }

    public class RoomEquipmentDtoMappingProfile : Profile
    {
        public RoomEquipmentDtoMappingProfile()
        {
            _ = CreateMap<RoomEquipment, RoomEquipmentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Equipment.SelectValue.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .AfterMap<LocalizeAction<RoomEquipment, RoomEquipmentDto>>();
        }
    }
}