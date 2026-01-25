using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.ProjectDomain.Commands
{
    public static class SetProjectSetlist
    {
        public class Command : IRequest
        {
            public Guid ProjectId { get; set; }
            public Guid? SetlistId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ProjectId)
                    .EntityExists<Command, Project>(arpaContext);

                RuleFor(c => c.SetlistId)
                    .EntityExists<Command, Setlist>(arpaContext)
                    .When(c => c.SetlistId.HasValue);
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Project project = await _arpaContext.Projects.FindAsync([request.ProjectId], cancellationToken);

                project.SetSetlist(request.SetlistId);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(Project));
            }
        }
    }
}
