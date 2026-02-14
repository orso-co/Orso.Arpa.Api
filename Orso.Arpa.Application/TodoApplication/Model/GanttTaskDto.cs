using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.TodoDomain.Enums;

namespace Orso.Arpa.Application.TodoApplication.Model
{
    public class GanttTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TodoStatus Status { get; set; }
        public TodoPriority Priority { get; set; }
        public int SortOrder { get; set; }
        public Guid? AssigneeId { get; set; }
        public string AssigneeDisplayName { get; set; }
        public Guid? ParentTodoId { get; set; }
        public Guid? ProjectId { get; set; }
        public double Progress { get; set; }
        public IList<GanttDependencyDto> Dependencies { get; set; } = [];
    }

    public class GanttDependencyDto
    {
        public Guid DependsOnTaskId { get; set; }
        public DependencyType Type { get; set; }
        public int LagDays { get; set; }
    }

    public class GanttDateUpdateDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class GanttDependencyCreateDto
    {
        public Guid DependentTaskId { get; set; }
        public Guid DependsOnTaskId { get; set; }
        public DependencyType Type { get; set; } = DependencyType.FinishToStart;
        public int LagDays { get; set; }
    }

    public class GanttSortOrderUpdateDto
    {
        public int SortOrder { get; set; }
    }
}
