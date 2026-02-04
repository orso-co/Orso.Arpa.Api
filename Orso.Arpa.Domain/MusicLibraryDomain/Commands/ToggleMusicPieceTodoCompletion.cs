using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class ToggleMusicPieceTodoCompletion
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicPieceTodo>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var todo = await _arpaContext.MusicPieceTodos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (todo == null)
                {
                    throw new NotFoundException(nameof(MusicPieceTodo), request.Id.ToString());
                }

                todo.ToggleCompletion();

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return todo.IsCompleted;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicPieceTodo));
            }
        }
    }
}
