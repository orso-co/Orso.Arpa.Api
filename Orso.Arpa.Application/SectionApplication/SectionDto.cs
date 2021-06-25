using System;
using AutoMapper;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Translation;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionDto
    {
        [Translate(nameof(SectionDto))]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public byte InstrumentPartCount { get; set; }
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
