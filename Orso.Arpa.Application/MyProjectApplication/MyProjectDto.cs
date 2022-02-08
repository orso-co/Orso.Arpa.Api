using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.ProjectApplication;
using static Orso.Arpa.Domain.Logic.MyProjects.List;

namespace Orso.Arpa.Application.MyProjectApplication;

public class MyProjectDto
{
    public ProjectDto Project { get; set; }
    public IList<MyProjectParticipationDto> Participations { get; set; } = new List<MyProjectParticipationDto>();
}

public class MyProjectDtoMappingProfile : Profile
{
    public MyProjectDtoMappingProfile()
    {
        CreateMap<MyProjectGrouping, MyProjectDto>()
            .ForMember(d => d.Participations, opt => opt.MapFrom(src => src.ProjectParticipations));
    }
}
