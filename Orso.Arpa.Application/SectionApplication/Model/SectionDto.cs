using System;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Infrastructure.Localization;
using static System.Collections.Specialized.BitVector32;

namespace Orso.Arpa.Application.SectionApplication.Model
{
    public class SectionDto
    {
        [Translate(LocalizationKeys.SECTION)]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public byte InstrumentPartCount { get; set; }
    }

    public class SectionDtoMappingProfile : Profile
    {
        public SectionDtoMappingProfile()
        {
            _ = CreateMap<Section, SectionDto>()
                .AfterMap<LocalizeAction<Section, SectionDto>>();

            _ = CreateMap<SectionDto, Section>();
        }
    }
}
