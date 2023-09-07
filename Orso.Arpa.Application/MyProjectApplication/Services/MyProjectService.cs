using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MyProjectApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MyProjects;
using Orso.Arpa.Domain.Logic.ProjectParticipations;
using static Orso.Arpa.Domain.Logic.ProjectParticipations.GetForPerson;

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

    public async Task<MyProjectListDto> GetMyProjectsAsync(int? offset, int? limit, bool includeCompleted)
    {
        var query = new GetForPerson.Query
        {
            PersonId = _userAccessor.PersonId,
            Offset = offset,
            Limit = limit,
            IncludeCompleted = includeCompleted
        };
        Tuple<IEnumerable<PersonProjectParticipationGrouping>, int> result = await _mediator.Send(query);

        return new MyProjectListDto
        {
            UserProjects = _mapper.Map<IList<MyProjectDto>>(result.Item1),
            TotalRecordsCount = result.Item2
        };
    }

    public async Task<MyProjectParticipationDto> SetProjectParticipationStatus(MyProjectParticipationModifyDto myProjectParticipationModifyDto)
    {
        SetProjectParticipationStatus.Command command = _mapper
            .Map<SetProjectParticipationStatus.Command>(myProjectParticipationModifyDto);

        ProjectParticipation projectParticipation = await _mediator.Send(command);

        var notification = new ProjectParticipationChangedNotification
        {
            ProjectParticipation = projectParticipation,
            ChangedByPerformer = true,
        };
        await _mediator.Publish(notification);

        return _mapper.Map<MyProjectParticipationDto>(projectParticipation);
    }
}
