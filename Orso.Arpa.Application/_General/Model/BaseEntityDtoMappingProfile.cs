using System;
using AutoMapper;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Application.General.Model
{
    public class BaseEntityDtoMappingProfile : Profile
    {
        public BaseEntityDtoMappingProfile()
        {
            CreateMap<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.Equals(DateTime.MinValue) ? (DateTime?)null : src.CreatedAt));
            CreateMap<BaseEntityDto, BaseEntityDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.Equals(DateTime.MinValue) ? null : src.CreatedAt));
        }
    }
}
