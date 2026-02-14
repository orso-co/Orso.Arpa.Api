using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.TodoDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.TodoDomain.Commands
{
    public static class CreateTodoComment
    {
        public class Command : ICreateCommand<TodoComment>
        {
            public Guid TodoItemId { get; set; }
            public Guid AuthorId { get; set; }
            public string Content { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.TodoItemId)
                    .MustAsync(async (id, cancellation) =>
                        await arpaContext.EntityExistsAsync<TodoItem>(id, cancellation))
                    .WithMessage("Todo item not found.");

                _ = RuleFor(c => c.Content)
                    .NotEmpty()
                    .MaximumLength(2000);
            }
        }
    }
}
