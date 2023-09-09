using System;
using AutoMapper;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class ReducedPersonDto
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }
    }

    public class ReducedPersonDtoMappingProfile : Profile
    {
        public ReducedPersonDtoMappingProfile()
        {
            CreateMap<Person, ReducedPersonDto>();
        }
    }
}
