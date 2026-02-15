using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class RemoveInstrumentationPosition
    {
        public class Command : IRequest
        {
            public Guid InstrumentationId { get; set; }
            public Guid PositionId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.InstrumentationId)
                    .EntityExists<Command, Instrumentation>(context);

                RuleFor(c => c.PositionId)
                    .EntityExists<Command, InstrumentationPosition>(context);
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
                InstrumentationPosition position = await _context.InstrumentationPositions.FindAsync([request.PositionId], cancellationToken)
                    ?? throw new NotFoundException(nameof(InstrumentationPosition), nameof(request.PositionId));

                _context.Remove(position);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
