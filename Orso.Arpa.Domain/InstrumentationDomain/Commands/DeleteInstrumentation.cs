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
    public static class DeleteInstrumentation
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Instrumentation>(context);
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
                Instrumentation instrumentation = await _context.Instrumentations.FindAsync([request.Id], cancellationToken)
                    ?? throw new NotFoundException(nameof(Instrumentation), nameof(request.Id));

                _context.Remove(instrumentation);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
