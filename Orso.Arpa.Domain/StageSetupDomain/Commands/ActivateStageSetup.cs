using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class ActivateStageSetup
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid ProjectId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<StageSetup>(s => s.Id == id && s.ProjectId == command.ProjectId, cancellation))
                    .WithMessage("Stage setup could not be found")
                    .WithErrorCode("404");
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
                // Deactivate all other setups for this project
                var otherSetups = await _arpaContext.Set<StageSetup>()
                    .Where(s => s.ProjectId == request.ProjectId && s.Id != request.Id && s.IsActive)
                    .ToListAsync(cancellationToken);

                foreach (var setup in otherSetups)
                {
                    setup.Deactivate();
                }

                // Activate the requested setup
                var setupToActivate = await _arpaContext.FindAsync<StageSetup>(new object[] { request.Id }, cancellationToken);
                setupToActivate.Activate();

                await _arpaContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
