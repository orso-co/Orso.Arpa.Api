using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model
{
    public class CurriculumVitaeReferenceDto : BaseEntityDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid? TypeId { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }
        public SelectValueDto Type { get; set; }
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
