using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomDto : BaseEntityDto
    {
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Name { get; set; }
        public CeilingHeight? CeilingHeight { get; set; }
        public SelectValueDto Capacity { get; set; }
        public int? SizeInSquareMeters { get; set; }
        public List<RoomSectionDto> AvailableInstruments { get; set; } = [];
        public List<RoomEquipmentDto> AvailableEquipment { get; set; } = [];
    }

    public class RoomDtoMappingProfile : Profile
    {
        public RoomDtoMappingProfile()
        {
            CreateMap<Room, RoomDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.AvailableInstruments, opt => opt.MapFrom(src => src.RoomSections))
                .ForMember(dest => dest.AvailableEquipment, opt => opt.MapFrom(src => src.RoomEquipments))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity));
        }
    }
}
