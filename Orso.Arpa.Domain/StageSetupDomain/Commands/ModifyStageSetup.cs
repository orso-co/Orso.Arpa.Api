using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class ModifyStageSetup
    {
        public class Command : IModifyCommand<StageSetup>
        {
            public Guid Id { get; set; }
            public Guid ProjectId { get; set; }
            public string Name { get; set; }
            public int CanvasWidth { get; set; }
            public int CanvasHeight { get; set; }
            public bool IsActive { get; set; }
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

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.CanvasWidth)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(10000);

                RuleFor(c => c.CanvasHeight)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(10000);
            }
        }
    }
}
