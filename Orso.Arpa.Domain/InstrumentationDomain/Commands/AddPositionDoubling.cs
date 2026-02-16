using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class AddPositionDoubling
    {
        public class Command : IRequest<InstrumentationPositionDoubling>
        {
            public Guid PositionId { get; set; }
            public Guid SectionId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.PositionId)
                    .EntityExists<Command, InstrumentationPosition>(context);

                RuleFor(c => c.SectionId)
                    .EntityExists<Command, Section>(context);
            }
        }

        public class Handler : IRequestHandler<Command, InstrumentationPositionDoubling>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<InstrumentationPositionDoubling> Handle(Command request, CancellationToken cancellationToken)
            {
                var doubling = new InstrumentationPositionDoubling(
                    Guid.NewGuid(),
                    request.PositionId,
                    request.SectionId);

                _context.InstrumentationPositionDoublings.Add(doubling);
                await _context.SaveChangesAsync(cancellationToken);

                return doubling;
            }
        }
    }
}
