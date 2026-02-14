using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TodoDomain.Enums;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Domain.TodoDomain.Commands
{
    public static class ModifyTodo
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public TodoPriority Priority { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? EstimatedTime { get; set; }
            public Guid? AssigneeId { get; set; }
            public int? ReminderDaysBefore { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) =>
                        await arpaContext.EntityExistsAsync<TodoItem>(id, cancellation))
                    .WithMessage("Todo item not found.");

                _ = RuleFor(c => c.Title)
                    .NotEmpty()
                    .MaximumLength(500);

                _ = RuleFor(c => c.Description)
                    .MaximumLength(4000);

                _ = RuleFor(c => c.ReminderDaysBefore)
                    .InclusiveBetween(1, 365)
                    .When(c => c.ReminderDaysBefore.HasValue);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                TodoItem todo = await _arpaContext.GetByIdAsync<TodoItem>(request.Id, cancellationToken);
                todo.Update(request);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                }

                return Unit.Value;
            }
        }
    }
}
