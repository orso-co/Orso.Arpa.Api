using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationPositionDto : BaseEntityDto
    {
        public Guid InstrumentationId { get; set; }
        public Guid SectionId { get; set; }
        public SectionDto Section { get; set; }
        public int Quantity { get; set; }
        public Guid? QualificationId { get; set; }
        public int SortOrder { get; set; }
        public string Label { get; set; }
        public string Comment { get; set; }
        public IList<InstrumentationPositionDoublingDto> Doublings { get; set; } = [];
    }

    public class InstrumentationPositionDtoMappingProfile : Profile
    {
        public InstrumentationPositionDtoMappingProfile()
        {
            CreateMap<InstrumentationPosition, InstrumentationPositionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
