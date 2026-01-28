using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class SetStageSetupVisibility
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid ProjectId { get; set; }
            public bool IsVisibleToPerformers { get; set; }
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
                var setup = await _arpaContext.FindAsync<StageSetup>(new object[] { request.Id }, cancellationToken);

                // Use reflection to set the property (since it's private set)
                // Or better: add a method to the entity
                var modifyCommand = new ModifyStageSetup.Command
                {
                    Id = setup.Id,
                    ProjectId = setup.ProjectId,
                    Name = setup.Name,
                    CanvasWidth = setup.CanvasWidth,
                    CanvasHeight = setup.CanvasHeight,
                    IsActive = setup.IsActive,
                    IsVisibleToPerformers = request.IsVisibleToPerformers
                };

                setup.Update(modifyCommand);

                await _arpaContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
