using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class CreateStageSetupEquipment
    {
        public class Command : ICreateCommand<StageSetupEquipment>
        {
            public Guid StageSetupId { get; set; }
            public string EquipmentId { get; set; }
            public double PositionX { get; set; }
            public double PositionY { get; set; }
            public double Rotation { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.StageSetupId)
                    .EntityExists<Command, StageSetup>(arpaContext);

                RuleFor(c => c.EquipmentId)
                    .NotEmpty()
                    .MaximumLength(100);

                RuleFor(c => c.PositionX)
                    .InclusiveBetween(0, 100)
                    .WithMessage("PositionX must be between 0 and 100 (percentage)");

                RuleFor(c => c.PositionY)
                    .InclusiveBetween(0, 100)
                    .WithMessage("PositionY must be between 0 and 100 (percentage)");

                RuleFor(c => c.Rotation)
                    .InclusiveBetween(0, 360)
                    .WithMessage("Rotation must be between 0 and 360 degrees");
            }
        }
    }
}
