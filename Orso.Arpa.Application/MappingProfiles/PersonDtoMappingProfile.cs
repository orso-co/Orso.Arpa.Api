using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class PersonDtoMappingProfile : Profile
    {
        public PersonDtoMappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
