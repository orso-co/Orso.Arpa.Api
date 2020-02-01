using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Logic.Persons
{
    public class PersonDto : BaseEntityDto
    {
        public string GivenName { get; set; }

        public string Surname { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
