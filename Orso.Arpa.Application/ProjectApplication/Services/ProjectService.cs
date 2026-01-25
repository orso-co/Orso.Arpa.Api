using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AppointmentApplication.Model;
using Orso.Arpa.Application.AppointmentParticipationApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.ProjectApplication.Interfaces;
using Orso.Arpa.Application.ProjectApplication.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.MusicianProfileDomain.Queries;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Notifications;
using Orso.Arpa.Domain.ProjectDomain.Queries;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Queries;

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

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsWithParticipationsByIdAsync(Guid id)
        {
            var query = new ListAppointmentsForProject.Query { ProjectId = id };
            IOrderedQueryable<Appointment> appointments = await _mediator.Send(query);
            List<Appointment> appointmentList = await appointments.ToListAsync();

            var treeQuery = new ListFlattenedSectionTree.Query();
            IEnumerable<ITree<Section>> flattenedTree = await _mediator.Send(treeQuery);

            // Process appointments in parallel with concurrency limit to avoid DB overload
            const int maxConcurrency = 10;
            using var semaphore = new SemaphoreSlim(maxConcurrency);
            var tasks = appointmentList.Select(async appointment =>
            {
                await semaphore.WaitAsync();
                try
                {
                    AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);
                    await AddParticipationsAsync(dto, appointment, flattenedTree);
                    return dto;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            AppointmentDto[] result = await Task.WhenAll(tasks);
            return result;
        }

        private async Task AddParticipationsAsync(
            AppointmentDto dto,
            Appointment appointment,
            IEnumerable<ITree<Section>> flattenedTree)
        {
            var query = new ListParticipationsForAppointment.Query
            {
                Appointment = appointment,
                SectionTree = flattenedTree,
            };

            IEnumerable<ListParticipationsForAppointment.PersonGrouping> personGrouping = await _mediator.Send(query);

            dto.Participations = _mapper.Map<IList<AppointmentParticipationListItemDto>>(personGrouping);
        }

        public async Task SetSetlistAsync(Guid projectId, Guid? setlistId)
        {
            var command = new SetProjectSetlist.Command
            {
                ProjectId = projectId,
                SetlistId = setlistId
            };
            await _mediator.Send(command);
        }
    }
}
