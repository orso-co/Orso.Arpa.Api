using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.ChatDomain.Model;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.TodoDomain.Commands;
using Orso.Arpa.Domain.TodoDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TodoDomain.Model
{
    public class TodoItem : BaseEntity
    {
        public TodoItem(Guid? id, CreateTodo.Command command) : base(id)
        {
            Title = command.Title;
            Description = command.Description;
            Status = TodoStatus.Open;
            Priority = command.Priority ?? TodoPriority.Medium;
            DueDate = command.DueDate;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            EstimatedTime = command.EstimatedTime;
            SortOrder = command.SortOrder;
            CreatorId = command.CreatorId;
            AssigneeId = command.AssigneeId;
            ParentTodoId = command.ParentTodoId;
            EntityType = command.EntityType;
            PersonId = command.PersonId;
            OrganizationId = command.OrganizationId;
            ProjectId = command.ProjectId;
            AppointmentId = command.AppointmentId;
            SourceChatMessageId = command.SourceChatMessageId;
            ReminderDaysBefore = command.ReminderDaysBefore;
        }

        [JsonConstructor]
        protected TodoItem()
        {
        }

        public void Update(ModifyTodo.Command command)
        {
            Title = command.Title;
            Description = command.Description;
            Priority = command.Priority;
            DueDate = command.DueDate;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            EstimatedTime = command.EstimatedTime;
            AssigneeId = command.AssigneeId;
            ReminderDaysBefore = command.ReminderDaysBefore;
        }

        public void UpdateStatus(TodoStatus status)
        {
            Status = status;
            if (status == TodoStatus.Done)
            {
                CompletedAt = DateTime.UtcNow;
            }
            else
            {
                CompletedAt = null;
            }
        }

        public void UpdateAssignee(Guid? assigneeId)
        {
            AssigneeId = assigneeId;
        }

        public void UpdateDates(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public void UpdateSortOrder(int sortOrder)
        {
            SortOrder = sortOrder;
        }

        public void SetReminderSent()
        {
            ReminderSentAt = DateTime.UtcNow;
        }

        // Properties
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TodoStatus Status { get; private set; }
        public TodoPriority Priority { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public int? EstimatedTime { get; private set; }
        public int SortOrder { get; private set; }

        // Creator & Assignee
        public Guid CreatorId { get; private set; }
        public virtual User Creator { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public virtual User Assignee { get; private set; }

        // Subtask hierarchy
        public Guid? ParentTodoId { get; private set; }
        public virtual TodoItem ParentTodo { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<TodoItem> SubTasks { get; private set; } = new HashSet<TodoItem>();

        // Polymorphic entity linking
        public TodoEntityType EntityType { get; private set; }
        public Guid? PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid? OrganizationId { get; private set; }
        public virtual Organization Organization { get; private set; }
        public Guid? ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
        public Guid? AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }

        // Chat integration
        public Guid? ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }
        public Guid? SourceChatMessageId { get; private set; }
        public virtual ChatMessage SourceChatMessage { get; private set; }

        // Reminder
        public int? ReminderDaysBefore { get; private set; }
        public DateTime? ReminderSentAt { get; private set; }

        // Comments
        [CascadingSoftDelete]
        public virtual ICollection<TodoComment> Comments { get; private set; } = new HashSet<TodoComment>();

        // Dependencies (Gantt)
        public virtual ICollection<TodoDependency> DependentOn { get; private set; } = new HashSet<TodoDependency>();
        public virtual ICollection<TodoDependency> DependedOnBy { get; private set; } = new HashSet<TodoDependency>();
    }
}
