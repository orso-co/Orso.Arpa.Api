using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.TodoApplication.Interfaces;
using Orso.Arpa.Application.TodoApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TodoDomain.Commands;
using Orso.Arpa.Domain.TodoDomain.Enums;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Application.TodoApplication.Services
{
    public class TodoService : ITodoService
    {
        private readonly IArpaContext _arpaContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TodoService(IArpaContext arpaContext, IMediator mediator, IMapper mapper)
        {
            _arpaContext = arpaContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAsync(TodoStatus? status, TodoPriority? priority, Guid? assigneeId, TodoEntityType? entityType)
        {
            IQueryable<TodoItem> query = _arpaContext.TodoItems
                .Where(t => !t.Deleted);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);
            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);
            if (assigneeId.HasValue)
                query = query.Where(t => t.AssigneeId == assigneeId.Value);
            if (entityType.HasValue)
                query = query.Where(t => t.EntityType == entityType.Value);

            List<TodoItem> items = await query
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TodoItemDto>>(items);
        }

        public async Task<IEnumerable<TodoItemDto>> GetMyTodosAsync(Guid userId)
        {
            List<TodoItem> items = await _arpaContext.TodoItems
                .Where(t => !t.Deleted && (t.CreatorId == userId || t.AssigneeId == userId) && t.Status != TodoStatus.Done)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto> GetByIdAsync(Guid id)
        {
            TodoItem item = await _arpaContext.GetByIdAsync<TodoItem>(id, default);
            return _mapper.Map<TodoItemDto>(item);
        }

        public async Task<TodoItemDto> CreateAsync(TodoCreateDto dto, Guid creatorId)
        {
            CreateTodo.Command command = _mapper.Map<CreateTodo.Command>(dto);
            command.CreatorId = creatorId;

            TodoItem created = await _mediator.Send(command);
            return _mapper.Map<TodoItemDto>(created);
        }

        public async Task ModifyAsync(TodoModifyDto dto)
        {
            ModifyTodo.Command command = _mapper.Map<ModifyTodo.Command>(dto);
            await _mediator.Send(command);
        }

        public async Task UpdateStatusAsync(Guid id, TodoStatus status)
        {
            await _mediator.Send(new UpdateTodoStatus.Command { Id = id, Status = status });
        }

        public async Task AssignAsync(Guid id, Guid? assigneeId)
        {
            await _mediator.Send(new AssignTodo.Command { Id = id, AssigneeId = assigneeId });
        }

        public async Task DeleteAsync(Guid id)
        {
            TodoItem todo = await _arpaContext.GetByIdAsync<TodoItem>(id, default);
            todo.Delete("system", DateTime.UtcNow);
            await _arpaContext.SaveChangesAsync(default);
        }

        public async Task<IEnumerable<TodoItemDto>> GetByEntityAsync(TodoEntityType entityType, Guid entityId)
        {
            IQueryable<TodoItem> query = _arpaContext.TodoItems
                .Where(t => !t.Deleted && t.EntityType == entityType);

            query = entityType switch
            {
                TodoEntityType.Person => query.Where(t => t.PersonId == entityId),
                TodoEntityType.Organization => query.Where(t => t.OrganizationId == entityId),
                TodoEntityType.Project => query.Where(t => t.ProjectId == entityId),
                TodoEntityType.Appointment => query.Where(t => t.AppointmentId == entityId),
                _ => query
            };

            List<TodoItem> items = await query
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto> CreateFromChatMessageAsync(TodoCreateDto dto, Guid creatorId, Guid chatMessageId)
        {
            CreateTodo.Command command = _mapper.Map<CreateTodo.Command>(dto);
            command.CreatorId = creatorId;
            command.SourceChatMessageId = chatMessageId;

            TodoItem created = await _mediator.Send(command);
            return _mapper.Map<TodoItemDto>(created);
        }

        public async Task<IEnumerable<TodoCommentDto>> GetCommentsAsync(Guid todoId)
        {
            List<TodoComment> comments = await _arpaContext.TodoComments
                .Where(c => !c.Deleted && c.TodoItemId == todoId)
                .OrderBy(c => c.PostedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TodoCommentDto>>(comments);
        }

        public async Task<TodoCommentDto> CreateCommentAsync(Guid todoId, TodoCommentCreateDto dto, Guid authorId)
        {
            CreateTodoComment.Command command = _mapper.Map<CreateTodoComment.Command>(dto);
            command.TodoItemId = todoId;
            command.AuthorId = authorId;

            TodoComment created = await _mediator.Send(command);
            return _mapper.Map<TodoCommentDto>(created);
        }

        // Gantt methods

        public async Task<IEnumerable<GanttTaskDto>> GetGanttTasksAsync(Guid? projectId)
        {
            IQueryable<TodoItem> query = _arpaContext.TodoItems
                .Where(t => !t.Deleted);

            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId.Value);

            List<TodoItem> items = await query
                .OrderBy(t => t.SortOrder)
                .ThenBy(t => t.StartDate)
                .ToListAsync();

            var ganttTasks = new List<GanttTaskDto>();
            foreach (TodoItem item in items)
            {
                // Load dependencies
                List<TodoDependency> dependencies = await _arpaContext.TodoDependencies
                    .Where(d => d.DependentTaskId == item.Id)
                    .ToListAsync();

                var ganttTask = new GanttTaskDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Status = item.Status,
                    Priority = item.Priority,
                    SortOrder = item.SortOrder,
                    AssigneeId = item.AssigneeId,
                    AssigneeDisplayName = item.Assignee?.Person != null
                        ? item.Assignee.Person.GivenName + " " + item.Assignee.Person.Surname
                        : null,
                    ParentTodoId = item.ParentTodoId,
                    ProjectId = item.ProjectId,
                    Progress = item.Status == TodoStatus.Done ? 1.0 : item.Status == TodoStatus.InProgress ? 0.5 : 0.0,
                    Dependencies = dependencies.Select(d => new GanttDependencyDto
                    {
                        DependsOnTaskId = d.DependsOnTaskId,
                        Type = d.Type,
                        LagDays = d.LagDays
                    }).ToList()
                };
                ganttTasks.Add(ganttTask);
            }

            return ganttTasks;
        }

        public async Task UpdateGanttDatesAsync(Guid id, DateTime? startDate, DateTime? endDate)
        {
            TodoItem todo = await _arpaContext.GetByIdAsync<TodoItem>(id, default);
            todo.UpdateDates(startDate, endDate);
            await _arpaContext.SaveChangesAsync(default);
        }

        public async Task CreateDependencyAsync(GanttDependencyCreateDto dto)
        {
            var dependency = new TodoDependency(dto.DependentTaskId, dto.DependsOnTaskId, dto.Type, dto.LagDays);
            _arpaContext.Add(dependency);
            await _arpaContext.SaveChangesAsync(default);
        }

        public async Task RemoveDependencyAsync(Guid dependentTaskId, Guid dependsOnTaskId)
        {
            TodoDependency dependency = await _arpaContext.TodoDependencies
                .FirstOrDefaultAsync(d => d.DependentTaskId == dependentTaskId && d.DependsOnTaskId == dependsOnTaskId);

            if (dependency != null)
            {
                _arpaContext.Remove(dependency);
                await _arpaContext.SaveChangesAsync(default);
            }
        }

        public async Task UpdateSortOrderAsync(Guid id, int sortOrder)
        {
            TodoItem todo = await _arpaContext.GetByIdAsync<TodoItem>(id, default);
            todo.UpdateSortOrder(sortOrder);
            await _arpaContext.SaveChangesAsync(default);
        }
    }
}
