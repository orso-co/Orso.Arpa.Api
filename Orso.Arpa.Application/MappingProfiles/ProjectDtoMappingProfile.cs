using AutoMapper;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MappingProfiles
{
    public class ProjectDtoMappingProfile : Profile
    {
        public ProjectDtoMappingProfile()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}
