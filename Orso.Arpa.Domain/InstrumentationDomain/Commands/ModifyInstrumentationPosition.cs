using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class ModifyInstrumentationPosition
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid InstrumentationId { get; set; }
            public Guid SectionId { get; set; }
            public int Quantity { get; set; }
            public Guid? QualificationId { get; set; }
            public string Label { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, InstrumentationPosition>(context);

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

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                InstrumentationPosition position = await _context.InstrumentationPositions.FindAsync([request.Id], cancellationToken)
                    ?? throw new NotFoundException(nameof(InstrumentationPosition), nameof(request.Id));

                position.SectionId = request.SectionId;
                position.Quantity = request.Quantity;
                position.QualificationId = request.QualificationId;
                position.Label = request.Label;
                position.Comment = request.Comment;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
