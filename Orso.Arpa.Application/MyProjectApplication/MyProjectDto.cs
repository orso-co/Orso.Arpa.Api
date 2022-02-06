using AutoMapper;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MyProjectApplication;

public class MyProjectDto
{
    public ProjectDto Project { get; set; }
    public ProjectParticipationDto Participation { get; set; }
}

public class MyProjectDtoMappingProfile : Profile
{
    public MyProjectDtoMappingProfile()
    {
        CreateMap<ProjectParticipation, MyProjectDto>()
            .ForMember(d => d.Participation, opt => opt.MapFrom(scr => scr));
    }
}
