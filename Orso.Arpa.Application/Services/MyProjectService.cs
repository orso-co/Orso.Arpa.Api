using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Logic.MyProjects;

namespace Orso.Arpa.Application.Services;

public class MyProjectService : IMyProjectService
{
    private readonly IUserAccessor _userAccessor;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MyProjectService(IUserAccessor userAccessor, IMediator mediator, IMapper mapper)
    {
        _userAccessor = userAccessor;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MyProjectDto>> GetMyProjectsAsync()
    {
        var query = new List.Query { PersonId = _userAccessor.PersonId };
        IEnumerable<List.MyProjectGrouping> result = await _mediator.Send(query);

        return _mapper.Map<IEnumerable<MyProjectDto>>(result);
    }

    public async Task<MyProjectParticipationDto> SetProjectParticipationStatus(MyProjectParticipationModifyDto myProjectParticipationModifyDto)
    {
        SetProjectParticipationStatus.Command command = _mapper
            .Map<SetProjectParticipationStatus.Command>(myProjectParticipationModifyDto);

        ProjectParticipation projectParticipation = await _mediator.Send(command);
        return _mapper.Map<MyProjectParticipationDto>(projectParticipation);
    }
}
