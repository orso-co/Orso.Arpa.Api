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
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class ReorderInstrumentationPositions
    {
        public class Command : IRequest
        {
            public Guid InstrumentationId { get; set; }
            public List<Guid> OrderedPositionIds { get; set; } = [];
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.InstrumentationId)
                    .EntityExists<Command, Instrumentation>(context);

                RuleFor(c => c.OrderedPositionIds)
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
                List<InstrumentationPosition> positions = await _context.InstrumentationPositions
                    .Where(p => p.InstrumentationId == request.InstrumentationId && request.OrderedPositionIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                int sortOrder = 10;
                foreach (Guid positionId in request.OrderedPositionIds)
                {
                    InstrumentationPosition position = positions.FirstOrDefault(p => p.Id == positionId);
                    if (position != null)
                    {
                        position.SortOrder = sortOrder;
                        sortOrder += 10;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
