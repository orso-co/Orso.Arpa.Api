using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class AddDocumentToMusicianProfile
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
                RuleFor(d => d.Id)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                RuleFor(d => d.DocumentId)
                    .Cascade(CascadeMode.Stop)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, m => m.Documents)

                    .MustAsync(async (dto, documentId, cancellation) => !await arpaContext.MusicianProfileDocuments
                        .AnyAsync(ar => ar.SelectValueMappingId == documentId && ar.MusicianProfileId == dto.Id, cancellation))
                    .WithMessage("The document is already linked to the musician profile");
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
                var availableDocument = new MusicianProfileDocument(request.Id, request.DocumentId);

                _arpaContext.MusicianProfileDocuments.Add(availableDocument);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicianProfileDocument));
            }
        }
    }
}
