using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Application.MyProjectApplication.Model;

public class MyProjectDto
{
    public ProjectDto Project { get; set; }
    public ProjectDto ParentProject { get; set; }
    public IList<MyProjectParticipationDto> Participations { get; set; } = [];
}

public class MyProjectDtoMappingProfile : Profile
{
    public MyProjectDtoMappingProfile()
    {
        _ = CreateMap<PersonProjectParticipationGrouping, MyProjectDto>()
            .ForMember(d => d.Participations, opt => opt.MapFrom(src => src.ProjectParticipations))
            .ForMember(d => d.ParentProject, opt => opt.MapFrom(src => src.Project.Parent));
    }
}
