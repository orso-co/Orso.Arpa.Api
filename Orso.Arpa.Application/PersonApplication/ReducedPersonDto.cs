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
    }

    public class ReducedPersonDtoMappingProfile : Profile
    {
        public ReducedPersonDtoMappingProfile()
        {
            CreateMap<Person, ReducedPersonDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.GivenName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname));
        }
    }
}
