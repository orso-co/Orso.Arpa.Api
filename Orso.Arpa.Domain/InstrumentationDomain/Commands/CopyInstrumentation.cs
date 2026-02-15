using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.InstrumentationDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Commands
{
    public static class CopyInstrumentation
    {
        public class Command : IRequest<Instrumentation>
        {
            public Guid SourceId { get; set; }
            public Guid? ProjectId { get; set; }
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext context)
            {
                RuleFor(c => c.SourceId)
                    .EntityExists<Command, Instrumentation>(context);

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
                Instrumentation source = await _context.Instrumentations
                    .Include(i => i.Positions)
                        .ThenInclude(p => p.Doublings)
                    .FirstOrDefaultAsync(i => i.Id == request.SourceId, cancellationToken)
                    ?? throw new NotFoundException(nameof(Instrumentation), nameof(request.SourceId));

                var copy = new Instrumentation(
                    Guid.NewGuid(),
                    request.Name ?? source.Name,
                    source.Description,
                    isTemplate: request.ProjectId == null,
                    projectId: request.ProjectId,
                    sourceTemplateId: source.Id);

                _context.Instrumentations.Add(copy);

                foreach (InstrumentationPosition sourcePos in source.Positions.OrderBy(p => p.SortOrder))
                {
                    var newPos = new InstrumentationPosition(
                        Guid.NewGuid(),
                        copy.Id,
                        sourcePos.SectionId,
                        sourcePos.Quantity,
                        sourcePos.QualificationId,
                        sourcePos.SortOrder,
                        sourcePos.Label,
                        sourcePos.Comment);

                    _context.InstrumentationPositions.Add(newPos);

                    foreach (InstrumentationPositionDoubling sourceDbl in sourcePos.Doublings)
                    {
                        var newDbl = new InstrumentationPositionDoubling(
                            Guid.NewGuid(),
                            newPos.Id,
                            sourceDbl.SectionId);

                        _context.InstrumentationPositionDoublings.Add(newDbl);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return copy;
            }
        }
    }
}
