using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.ProjectApplication.Interfaces;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Notifications;
using Orso.Arpa.Domain.ProjectDomain.Queries;

namespace Orso.Arpa.Application.ProjectApplication.Services
{
    public class ProjectService : BaseService<
        ProjectDto,
        Project,
        ProjectCreateDto,
        CreateProject.Command,
        ProjectModifyDto,
        ProjectModifyBodyDto,
        ModifyProject.Command
        >, IProjectService
    {
        public ProjectService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<ProjectDto>> GetAsync(bool includeCompleted)
        {
            Expression<Func<Project, bool>> predicate = includeCompleted
                ? default
                : p => !p.IsCompleted && p.Status != ProjectStatus.Cancelled;
            return await base.GetAsync(predicate: predicate);
        }

        public async Task<IEnumerable<ProjectParticipationDto>> GetParticipationsByIdAsync(Guid id)
        {
            var query = new ListParticipationsForProject.Query { ProjectId = id };
            IOrderedQueryable<ProjectParticipation> projectParticipations = await _mediator.Send(query);

            List<ProjectParticipation> list = await projectParticipations.ToListAsync();
            return _mapper.Map<IEnumerable<ProjectParticipationDto>>(list);
        }

        public async Task<IEnumerable<AppointmentListDto>> GetAppointmentsByIdAsync(Guid id)
        {
            var query = new ListAppointmentsForProject.Query { ProjectId = id };
            IOrderedQueryable<Appointment> appointments = await _mediator.Send(query);

            List<Appointment> list = await appointments.ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentListDto>>(list);
        }

        public async Task<ProjectParticipationDto> SetProjectParticipationAsync(SetProjectParticipationDto myProjectParticipationDto)
        {
            SetProjectParticipation.Command command = _mapper
                .Map<SetProjectParticipation.Command>(myProjectParticipationDto);

            ProjectParticipation projectParticipation = await _mediator.Send(command);

            var notification = new ProjectParticipationChangedNotification
            {
                ProjectParticipation = projectParticipation,
                ChangedByPerformer = false
            };
            await _mediator.Publish(notification);

            return _mapper.Map<ProjectParticipationDto>(projectParticipation);
        }
    }
}
