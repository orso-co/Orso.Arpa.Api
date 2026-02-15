using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationPositionDoublingDto : BaseEntityDto
    {
        public Guid InstrumentationPositionId { get; set; }
        public Guid SectionId { get; set; }
        public SectionDto Section { get; set; }
    }

    public class InstrumentationPositionDoublingDtoMappingProfile : Profile
    {
        public InstrumentationPositionDoublingDtoMappingProfile()
        {
            CreateMap<InstrumentationPositionDoubling, InstrumentationPositionDoublingDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
