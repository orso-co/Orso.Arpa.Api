using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectDto : BaseEntityDto
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? GenreId { get; set; }
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
