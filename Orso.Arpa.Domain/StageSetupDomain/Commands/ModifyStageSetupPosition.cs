using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class ModifyStageSetupPosition
    {
        public class Command : IModifyCommand<StageSetupPosition>
        {
            public Guid Id { get; set; }
            public Guid StageSetupId { get; set; }
            public double PositionX { get; set; }
            public double PositionY { get; set; }
            public int? Row { get; set; }
            public int? Stand { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<StageSetupPosition>(p => p.Id == id && p.StageSetupId == command.StageSetupId, cancellation))
                    .WithMessage("Stage setup position could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.PositionX)
                    .InclusiveBetween(0, 100)
                    .WithMessage("PositionX must be between 0 and 100 (percentage)");

                RuleFor(c => c.PositionY)
                    .InclusiveBetween(0, 100)
                    .WithMessage("PositionY must be between 0 and 100 (percentage)");

                RuleFor(c => c.Row)
                    .GreaterThan(0)
                    .When(c => c.Row.HasValue);

                RuleFor(c => c.Stand)
                    .GreaterThan(0)
                    .When(c => c.Stand.HasValue);
            }
        }
    }
}
