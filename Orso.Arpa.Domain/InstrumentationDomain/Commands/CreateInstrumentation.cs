using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class CreateInstrumentation
    {
        public class Command : IRequest<Instrumentation>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsTemplate { get; set; }
            public Guid? ProjectId { get; set; }
            public Guid? SourceTemplateId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.Description)
                    .MaximumLength(2000);

                RuleFor(c => c.ProjectId)
                    .EntityExists<Command, Project>(context)
                    .When(c => c.ProjectId.HasValue);
            }
        }

        public class Handler : IRequestHandler<Command, Instrumentation>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<Instrumentation> Handle(Command request, CancellationToken cancellationToken)
            {
                var instrumentation = new Instrumentation(
                    Guid.NewGuid(),
                    request.Name,
                    request.Description,
                    request.IsTemplate,
                    request.ProjectId,
                    request.SourceTemplateId);

                _context.Instrumentations.Add(instrumentation);
                await _context.SaveChangesAsync(cancellationToken);

                return instrumentation;
            }
        }
    }
}
