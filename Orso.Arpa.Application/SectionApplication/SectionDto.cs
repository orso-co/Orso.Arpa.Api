using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Infrastructure.Localization;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionDto
    {
        [Translate]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public byte InstrumentPartCount { get; set; }
    }

    public class SectionDtoMappingProfile : Profile
    {
        public SectionDtoMappingProfile()
        {
            CreateMap<Section, SectionDto>()
                .AfterMap<LocalizeAction<Section, SectionDto>>();

            CreateMap<SectionDto, Section>();
        }
    }
}
