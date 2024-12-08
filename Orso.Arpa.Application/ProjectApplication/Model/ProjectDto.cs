using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Application.UrlApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.ProjectApplication.Model
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
        public IList<UrlDto> Urls { get; set; } = [];
        public ProjectStatus Status { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsHiddenForPerformers { get; set; }
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
