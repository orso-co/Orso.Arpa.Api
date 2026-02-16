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
    public static class ModifyInstrumentation
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsTemplate { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Instrumentation>(context);

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.Description)
                    .MaximumLength(2000);
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

                instrumentation.Name = request.Name;
                instrumentation.Description = request.Description;
                instrumentation.IsTemplate = request.IsTemplate;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
