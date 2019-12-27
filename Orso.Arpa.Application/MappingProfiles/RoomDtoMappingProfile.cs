using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class RoomDtoMappingProfile : Profile
    {
        public RoomDtoMappingProfile()
        {
            CreateMap<Room, RoomDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
