using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class CreateStageSetupPosition
    {
        public class Command : ICreateCommand<StageSetupPosition>
        {
            public Guid StageSetupId { get; set; }
            public Guid MusicianProfileId { get; set; }
            public double PositionX { get; set; }
            public double PositionY { get; set; }
            public int? Row { get; set; }
            public int? Stand { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.StageSetupId)
                    .EntityExists<Command, StageSetup>(arpaContext);

                RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                // Ensure unique combination of StageSetupId and MusicianProfileId
                RuleFor(c => c)
                    .MustAsync(async (command, cancellation) => !await arpaContext
                        .EntityExistsAsync<StageSetupPosition>(
                            p => p.StageSetupId == command.StageSetupId && p.MusicianProfileId == command.MusicianProfileId,
                            cancellation))
                    .WithMessage("This musician is already positioned in this stage setup")
                    .WithErrorCode("400");

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
