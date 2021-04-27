using System;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionDto
    {
        private readonly IStringLocalizer<SectionDto> _localizer;
        public SectionDto(IStringLocalizer<SectionDto> localizer)
        {
            _localizer = localizer;
        }

        public SectionDto()
        {
            _localizer = null;
        }

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
