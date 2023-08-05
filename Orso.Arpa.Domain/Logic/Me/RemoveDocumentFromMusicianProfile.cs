using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class RemoveDocumentFromMusicianProfile
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
                    .MustAsync(async (dto, documentId, cancellation) => await arpaContext.MusicianProfileDocuments
                        .AnyAsync(ar => ar.SelectValueMappingId == documentId && ar.MusicianProfileId == dto.Id, cancellation))
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
                MusicianProfileDocument documentToRemove = await _arpaContext.MusicianProfileDocuments
                                    .FirstOrDefaultAsync(ar => ar.SelectValueMappingId == request.DocumentId && ar.MusicianProfileId == request.Id, cancellationToken);

                _arpaContext.MusicianProfileDocuments.Remove(documentToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicianProfileDocument));
            }
        }
    }
}
