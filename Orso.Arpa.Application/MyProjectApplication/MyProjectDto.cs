using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

namespace Orso.Arpa.Application.MyProjectApplication;

public class MyProjectDto
{
    public ProjectDto Project { get; set; }
    public ProjectDto ParentProject { get; set; }
    public IList<MyProjectParticipationDto> Participations { get; set; } = new List<MyProjectParticipationDto>();
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
