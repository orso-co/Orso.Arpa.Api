using System.Collections.Generic;
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
        public ReducedPersonDto ContactVia { get; set;}
        public IList<ReducedPersonDto> ContactsRecommended { get; set; } = new List<ReducedPersonDto>();
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
