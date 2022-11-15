using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class Create
    {
        public class Command : ICreateCommand<Project>
        {
            public string Title { get; set; }
            public string ShortTitle { get; set; }
            public string Description { get; set; }
            public string Code { get; set; }
            public Guid? TypeId { get; set; }
            public Guid? GenreId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public ProjectStatus? Status { get; set; }
            public Guid? ParentId { get; set; }
            public bool IsCompleted { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Code)
                    .MustAsync(async (code, cancellation) =>
                        !await arpaContext.Projects.AnyAsync(project => project.Code.ToLower() == code.ToLower(), cancellation))
                    .WithMessage("The specified project code is already in use. The project code needs to be unique.");

                _ = RuleFor(c => c.ParentId)
                    .EntityExists<Command, Project>(arpaContext);

                _ = RuleFor(d => d.GenreId)
                   .SelectValueMapping<Command, Project>(arpaContext, a => a.Genre);

                _ = RuleFor(d => d.TypeId)
                   .SelectValueMapping<Command, Project>(arpaContext, a => a.Type);
            }
        }
    }
}
