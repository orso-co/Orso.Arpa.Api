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
    public static class RemovePositionDoubling
    {
        public class Command : IRequest
        {
            public Guid PositionId { get; set; }
            public Guid DoublingId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.PositionId)
                    .EntityExists<Command, InstrumentationPosition>(context);

                RuleFor(c => c.DoublingId)
                    .EntityExists<Command, InstrumentationPositionDoubling>(context);
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
                InstrumentationPositionDoubling doubling = await _context.InstrumentationPositionDoublings.FindAsync([request.DoublingId], cancellationToken)
                    ?? throw new NotFoundException(nameof(InstrumentationPositionDoubling), nameof(request.DoublingId));

                _context.Remove(doubling);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
