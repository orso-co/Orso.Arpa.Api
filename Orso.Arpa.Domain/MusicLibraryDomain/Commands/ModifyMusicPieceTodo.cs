using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class ModifyMusicPieceTodo
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public DateTime? DueDate { get; set; }
            public IList<Guid> AssigneeIds { get; set; } = new List<Guid>();
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicPieceTodo>(arpaContext);

                RuleFor(c => c.Title)
                    .NotEmpty()
                    .MaximumLength(500);

                RuleForEach(c => c.AssigneeIds)
                    .MustAsync(async (id, cancellation) =>
                        await arpaContext.Users.AnyAsync(u => u.Id == id, cancellation))
                    .WithMessage("User does not exist");
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
                var todo = await _arpaContext.MusicPieceTodos
                    .Include(t => t.Assignees)
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (todo == null)
                {
                    throw new NotFoundException(nameof(MusicPieceTodo), request.Id.ToString());
                }

                todo.Update(request);

                // Update assignees
                todo.Assignees.Clear();
                foreach (var userId in request.AssigneeIds)
                {
                    var user = await _arpaContext.Users.FindAsync(new object[] { userId }, cancellationToken);
                    if (user != null)
                    {
                        todo.Assignees.Add(user);
                    }
                }

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicPieceTodo));
            }
        }
    }
}
