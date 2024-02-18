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
    public static class RemoveDocumentFromMyMusicianProfile
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid DocumentId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.DocumentId)
                    .MustAsync(async (dto, documentId, cancellation) => await arpaContext
                        .EntityExistsAsync<MusicianProfileDocument>(ar => ar.SelectValueMappingId == documentId && ar.MusicianProfileId == dto.Id, cancellation))
                    .WithMessage("The document is not linked to the musician profile");
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
                MusicianProfileDocument documentToRemove = await _arpaContext
                    .Set<MusicianProfileDocument>()
                    .FirstOrDefaultAsync(ar => ar.SelectValueMappingId == request.DocumentId && ar.MusicianProfileId == request.Id, cancellationToken);

                _arpaContext.Set<MusicianProfileDocument>().Remove(documentToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicianProfileDocument));
            }
        }
    }
}
