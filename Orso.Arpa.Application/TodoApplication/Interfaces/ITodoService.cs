using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.TodoApplication.Model;
using Orso.Arpa.Domain.TodoDomain.Enums;

namespace Orso.Arpa.Application.TodoApplication.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetAsync(TodoStatus? status, TodoPriority? priority, Guid? assigneeId, TodoEntityType? entityType);
        Task<IEnumerable<TodoItemDto>> GetMyTodosAsync(Guid userId);
        Task<TodoItemDto> GetByIdAsync(Guid id);
        Task<TodoItemDto> CreateAsync(TodoCreateDto dto, Guid creatorId);
        Task ModifyAsync(TodoModifyDto dto);
        Task UpdateStatusAsync(Guid id, TodoStatus status);
        Task AssignAsync(Guid id, Guid? assigneeId);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<TodoItemDto>> GetByEntityAsync(TodoEntityType entityType, Guid entityId);
        Task<TodoItemDto> CreateFromChatMessageAsync(TodoCreateDto dto, Guid creatorId, Guid chatMessageId);

        Task<IEnumerable<TodoCommentDto>> GetCommentsAsync(Guid todoId);
        Task<TodoCommentDto> CreateCommentAsync(Guid todoId, TodoCommentCreateDto dto, Guid authorId);

        // Gantt
        Task<IEnumerable<GanttTaskDto>> GetGanttTasksAsync(Guid? projectId);
        Task UpdateGanttDatesAsync(Guid id, DateTime? startDate, DateTime? endDate);
        Task CreateDependencyAsync(GanttDependencyCreateDto dto);
        Task RemoveDependencyAsync(Guid dependentTaskId, Guid dependsOnTaskId);
        Task UpdateSortOrderAsync(Guid id, int sortOrder);
    }
}
