using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.PersonApplication
{
    public class PersonDto : BaseEntityDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
    }

    public class PersonDtoMappingProfile : Profile
    {
        public PersonDtoMappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
