using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class SelectValueDtoMappingProfile : Profile
    {
        public SelectValueDtoMappingProfile()
        {
            CreateMap<SelectValue, SelectValueDto>();
        }
    }
}
