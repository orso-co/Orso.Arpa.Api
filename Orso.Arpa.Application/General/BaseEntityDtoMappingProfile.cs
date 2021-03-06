using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.General
{
    public class BaseEntityDtoMappingProfile : Profile
    {
        public BaseEntityDtoMappingProfile()
        {
            CreateMap<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.Equals(DateTime.MinValue) ? (DateTime?)null : src.CreatedAt));
        }
    }
}
