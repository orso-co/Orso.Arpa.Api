using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class BaseEntityDtoMappingProfile : Profile
    {
        public BaseEntityDtoMappingProfile()
        {
            CreateMap<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToIsoString()))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt != null ? src.ModifiedAt.Value.ToIsoString() : null));
        }
    }
}
