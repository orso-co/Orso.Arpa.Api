using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class AddInstrumentationPosition
    {
        public class Command : IRequest<InstrumentationPosition>
        {
            public Guid InstrumentationId { get; set; }
            public Guid SectionId { get; set; }
            public int Quantity { get; set; } = 1;
            public Guid? QualificationId { get; set; }
            public string Label { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.InstrumentationId)
                    .EntityExists<Command, Instrumentation>(context);

                RuleFor(c => c.SectionId)
                    .EntityExists<Command, Section>(context);

                RuleFor(c => c.Quantity)
                    .GreaterThan(0);

                RuleFor(c => c.Label)
                    .MaximumLength(200);

                RuleFor(c => c.Comment)
                    .MaximumLength(1000);
            }
        }

        public class Handler : IRequestHandler<Command, InstrumentationPosition>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<InstrumentationPosition> Handle(Command request, CancellationToken cancellationToken)
            {
                int maxSortOrder = await _context.InstrumentationPositions
                    .Where(p => p.InstrumentationId == request.InstrumentationId)
                    .MaxAsync(p => (int?)p.SortOrder, cancellationToken) ?? 0;

                var position = new InstrumentationPosition(
                    Guid.NewGuid(),
                    request.InstrumentationId,
                    request.SectionId,
                    request.Quantity,
                    request.QualificationId,
                    maxSortOrder + 10,
                    request.Label,
                    request.Comment);

                _context.InstrumentationPositions.Add(position);
                await _context.SaveChangesAsync(cancellationToken);

                return position;
            }
        }
    }
}
