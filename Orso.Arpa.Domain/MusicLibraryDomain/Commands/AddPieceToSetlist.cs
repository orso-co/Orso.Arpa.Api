using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Commands
{
    public static class AddPieceToSetlist
    {
        public class Command : IRequest<SetlistPiece>
        {
            public Guid SetlistId { get; set; }
            public Guid MusicPieceId { get; set; }
            public string Notes { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.SetlistId)
                    .EntityExists<Command, Setlist>(context);

                RuleFor(c => c.MusicPieceId)
                    .EntityExists<Command, MusicPiece>(context);
            }
        }

        public class Handler : IRequestHandler<Command, SetlistPiece>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<SetlistPiece> Handle(Command request, CancellationToken cancellationToken)
            {
                // Get the next sort order
                int maxSortOrder = await _context.SetlistPieces
                    .Where(sp => sp.SetlistId == request.SetlistId)
                    .MaxAsync(sp => (int?)sp.SortOrder, cancellationToken) ?? 0;

                var setlistPiece = new SetlistPiece(
                    Guid.NewGuid(),
                    request.SetlistId,
                    request.MusicPieceId,
                    maxSortOrder + 10,
                    request.Notes);

                _context.SetlistPieces.Add(setlistPiece);
                await _context.SaveChangesAsync(cancellationToken);

                return setlistPiece;
            }
        }
    }
}
