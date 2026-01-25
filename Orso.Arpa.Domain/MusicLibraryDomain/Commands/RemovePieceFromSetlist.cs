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
    public static class RemovePieceFromSetlist
    {
        public class Command : IRequest
        {
            public Guid SetlistId { get; set; }
            public Guid SetlistPieceId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.SetlistId)
                    .EntityExists<Command, Setlist>(context);

                RuleFor(c => c.SetlistPieceId)
                    .EntityExists<Command, SetlistPiece>(context);
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                SetlistPiece setlistPiece = await _context.SetlistPieces.FindAsync([request.SetlistPieceId], cancellationToken)
                    ?? throw new NotFoundException(nameof(SetlistPiece), nameof(request.SetlistPieceId));

                _context.SetlistPieces.Remove(setlistPiece);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
