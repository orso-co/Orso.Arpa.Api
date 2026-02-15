using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? SourceTemplateId { get; set; }
        public IList<InstrumentationPositionDto> Positions { get; set; } = [];
    }

    public class InstrumentationDtoMappingProfile : Profile
    {
        public InstrumentationDtoMappingProfile()
        {
            CreateMap<Instrumentation, InstrumentationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
