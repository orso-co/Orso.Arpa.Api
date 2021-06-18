using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication
{
    public class CurriculumVitaeReferenceDto : BaseEntityDto
    {
        public CurriculumVitaeReferenceDto(Guid id, string timeSpan, string institution, Guid typeId, string description, byte sortOrder, string createdBy, DateTime createdAt)
        {
            Id = id;
            TimeSpan = timeSpan;
            Institution = institution;
            TypeId = typeId;
            Description = description;
            SortOrder = sortOrder;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
        public CurriculumVitaeReferenceDto() { }

        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid TypeId { get; set; }
        public string Description { get; set; }
        public byte SortOrder { get; set; }
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
