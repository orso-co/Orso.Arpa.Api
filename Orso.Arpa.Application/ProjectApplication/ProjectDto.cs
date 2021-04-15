using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? GenreId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IList<UrlDto> Urls { get; set; } = new List<UrlDto>();
        public Guid? StateId { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ProjectDtoMappingProfile : Profile
    {
        public ProjectDtoMappingProfile()
        {
            CreateMap<Project, ProjectDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
