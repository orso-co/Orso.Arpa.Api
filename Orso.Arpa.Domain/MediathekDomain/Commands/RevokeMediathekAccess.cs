using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Commands
{
    public static class RevokeMediathekAccess
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) => await arpaContext.EntityExistsAsync<MediathekAccess>(id, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Mediathek access could not be found.");
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                MediathekAccess access = await _arpaContext.GetByIdAsync<MediathekAccess>(request.Id, cancellationToken);
                access.Revoke();
                await _arpaContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
