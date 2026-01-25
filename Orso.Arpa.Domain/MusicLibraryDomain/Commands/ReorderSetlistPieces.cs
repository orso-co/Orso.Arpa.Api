using System;
using System.Collections.Generic;
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
    public static class ReorderSetlistPieces
    {
        public class Command : IRequest
        {
            public Guid SetlistId { get; set; }
            /// <summary>
            /// List of SetlistPiece IDs in the desired order.
            /// </summary>
            public List<Guid> OrderedPieceIds { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.SetlistId)
                    .EntityExists<Command, Setlist>(context);

                RuleFor(c => c.OrderedPieceIds)
                    .NotEmpty();
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
                List<SetlistPiece> pieces = await _context.SetlistPieces
                    .Where(sp => sp.SetlistId == request.SetlistId && request.OrderedPieceIds.Contains(sp.Id))
                    .ToListAsync(cancellationToken);

                int sortOrder = 10;
                foreach (Guid pieceId in request.OrderedPieceIds)
                {
                    SetlistPiece piece = pieces.FirstOrDefault(p => p.Id == pieceId);
                    if (piece != null)
                    {
                        piece.SortOrder = sortOrder;
                        sortOrder += 10;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
