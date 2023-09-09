using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.MyProjectApplication.Interfaces;
using Orso.Arpa.Application.MyProjectApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Notifications;
using Orso.Arpa.Domain.ProjectDomain.Queries;

namespace Orso.Arpa.Application.MyProjectApplication.Services;

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
        var query = new GetProjectParticipationsForPerson.Query
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
        SetMyProjectParticipationStatus.Command command = _mapper
            .Map<SetMyProjectParticipationStatus.Command>(myProjectParticipationModifyDto);

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
