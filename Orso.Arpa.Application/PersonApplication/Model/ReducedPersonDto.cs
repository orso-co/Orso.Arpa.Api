using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.PersonApplication
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
