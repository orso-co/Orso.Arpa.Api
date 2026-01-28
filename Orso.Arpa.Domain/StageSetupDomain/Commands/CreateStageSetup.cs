using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.StageSetupDomain.Commands
{
    public static class CreateStageSetup
    {
        public class Command : ICreateCommand<StageSetup>
        {
            public Guid ProjectId { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
            public string StoragePath { get; set; }
            public string ContentType { get; set; }
            public long FileSize { get; set; }
            public int CanvasWidth { get; set; }
            public int CanvasHeight { get; set; }
            public bool IsActive { get; set; }
            public bool IsVisibleToPerformers { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ProjectId)
                    .EntityExists<Command, Project>(arpaContext);

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(200);

                RuleFor(c => c.FileName)
                    .NotEmpty()
                    .MaximumLength(500);

                RuleFor(c => c.StoragePath)
                    .NotEmpty()
                    .MaximumLength(1000);

                RuleFor(c => c.ContentType)
                    .NotEmpty()
                    .MaximumLength(100);

                RuleFor(c => c.FileSize)
                    .GreaterThan(0);

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
