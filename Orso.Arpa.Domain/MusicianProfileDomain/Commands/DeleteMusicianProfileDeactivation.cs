using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Commands
{
    public static class DeleteMusicianProfileDeactivation
    {
        public class Command : IRequest
        {
            public Guid MusicianProfileId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicianProfileId)
                    .MustAsync(async (command, musicianProfileId, cancellation) =>
                        await arpaContext.EntityExistsAsync<MusicianProfileDeactivation>(d => d.MusicianProfileId == musicianProfileId, cancellation))
                    .WithErrorCode("404")
                    .WithMessage($"{typeof(MusicianProfileDeactivation).Name} could not be found.");
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
                MusicianProfileDeactivation entityToDelete = await _arpaContext.MusicianProfileDeactivations.SingleAsync(d => d.MusicianProfileId == request.MusicianProfileId, cancellationToken);

                _arpaContext.Remove(entityToDelete);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicianProfileDeactivation));
            }
        }
    }
}
