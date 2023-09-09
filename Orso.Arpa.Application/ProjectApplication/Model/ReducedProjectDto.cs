using System;
using AutoMapper;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.ProjectApplication.Model
{
    public class ReducedProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }

    public class ReducedProjectDtoMappingProfile : Profile
    {
        public ReducedProjectDtoMappingProfile()
        {
            CreateMap<Project, ReducedProjectDto>();
        }
    }
}
