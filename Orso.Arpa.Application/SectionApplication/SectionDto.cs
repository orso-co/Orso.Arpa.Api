using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class SectionDtoMappingProfile : Profile
    {
        public SectionDtoMappingProfile()
        {
            CreateMap<Section, SectionDto>();

            CreateMap<SectionDto, Section>();
        }
    }
}
