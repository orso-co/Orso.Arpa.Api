using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.EducationApplication
{
    public class EducationDto : BaseEntityDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid? TypeId { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }
    }

    public class EducationDtoMappingProfile : Profile
    {
        public EducationDtoMappingProfile()
        {
            CreateMap<Education, EducationDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
