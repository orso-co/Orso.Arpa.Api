using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public SelectValueDto Type { get; set; }
        public SelectValueDto Genre { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<UrlDto> Urls { get; set; } = new List<UrlDto>();
        public ProjectStatus Status { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ProjectDtoMappingProfile : Profile
    {
        public ProjectDtoMappingProfile()
        {
            _ = CreateMap<Project, ProjectDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
