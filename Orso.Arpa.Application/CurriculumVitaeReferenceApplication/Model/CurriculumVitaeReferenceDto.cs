using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication
{
    public class CurriculumVitaeReferenceDto : BaseEntityDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid? TypeId { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }
    }

    public class CurriculumVitaeReferenceDtoMappingProfile : Profile
    {
        public CurriculumVitaeReferenceDtoMappingProfile()
        {
            CreateMap<CurriculumVitaeReference, CurriculumVitaeReferenceDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
