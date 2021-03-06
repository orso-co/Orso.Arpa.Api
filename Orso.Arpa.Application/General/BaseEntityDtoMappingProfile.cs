using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.General
{
    public class BaseEntityDtoMappingProfile : Profile
    {
        public BaseEntityDtoMappingProfile()
        {
            CreateMap<BaseEntity, BaseEntityDto>();
        }
    }
}
