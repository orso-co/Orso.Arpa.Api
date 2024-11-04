using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Commands
{
    public static class AddDocumentToMyMusicianProfile
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

                    .MustAsync(async (dto, documentId, cancellation) => !await arpaContext
                        .Set<MusicianProfileDocument>()
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

                await _arpaContext.Set<MusicianProfileDocument>().AddAsync(availableDocument, cancellationToken);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicianProfileDocument));
            }
        }
    }
}
