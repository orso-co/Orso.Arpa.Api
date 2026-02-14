using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TodoDomain.Enums;
using Orso.Arpa.Domain.TodoDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.TodoDomain.Commands
{
    public static class CreateTodo
    {
        public class Command : ICreateCommand<TodoItem>
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public TodoPriority? Priority { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? EstimatedTime { get; set; }
            public int SortOrder { get; set; }
            public Guid CreatorId { get; set; }
            public Guid? AssigneeId { get; set; }
            public Guid? ParentTodoId { get; set; }
            public TodoEntityType EntityType { get; set; }
            public Guid? PersonId { get; set; }
            public Guid? OrganizationId { get; set; }
            public Guid? ProjectId { get; set; }
            public Guid? AppointmentId { get; set; }
            public Guid? SourceChatMessageId { get; set; }
            public int? ReminderDaysBefore { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.Title)
                    .NotEmpty()
                    .MaximumLength(500);

                _ = RuleFor(c => c.Description)
                    .MaximumLength(4000);

                _ = RuleFor(c => c.ParentTodoId)
                    .MustAsync(async (command, parentId, cancellation) =>
                    {
                        if (parentId == null) return true;

                        // Check parent exists
                        if (!await arpaContext.Set<TodoItem>().AnyAsync(t => t.Id == parentId, cancellation))
                            return false;

                        // Check max 3 levels: parent's parent's parent must be null
                        var parent = await arpaContext.Set<TodoItem>()
                            .Include(t => t.ParentTodo)
                            .ThenInclude(t => t.ParentTodo)
                            .FirstOrDefaultAsync(t => t.Id == parentId, cancellation);

                        if (parent?.ParentTodo?.ParentTodoId != null)
                            return false; // Would be 4th level

                        return true;
                    })
                    .WithMessage("Maximum subtask depth of 3 levels exceeded or parent does not exist.");

                _ = RuleFor(c => c.ReminderDaysBefore)
                    .InclusiveBetween(1, 365)
                    .When(c => c.ReminderDaysBefore.HasValue);
            }
        }
    }
}
