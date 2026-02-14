using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Domain.TodoDomain.Commands
{
    public static class AssignTodo
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid? AssigneeId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) =>
                        await arpaContext.EntityExistsAsync<TodoItem>(id, cancellation))
                    .WithMessage("Todo item not found.");
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
                todo.UpdateAssignee(request.AssigneeId);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                }

                return Unit.Value;
            }
        }
    }
}
