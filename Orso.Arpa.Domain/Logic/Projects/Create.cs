using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
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
            public string Number { get; set; }
            public Guid? TypeId { get; set; }
            public Guid? GenreId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public Guid? StateId { get; set; }
            public Guid? ParentId { get; set; }
            public bool IsCompleted { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Number)
                    .MustAsync(async (number, cancellation) =>
                        (!await arpaContext.Projects.AnyAsync(project =>
#pragma warning disable RCS1155 // Use StringComparison when comparing strings. -> ToLower() is used to allow ef core to perform the query on db server
                                        project.Number.ToLower() == number.ToLower(), cancellation)))
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
                    .WithMessage("The specified project number is already in use. The project number needs to be unique.");

                RuleFor(c => c.ParentId)
                    .EntityExists<Command, Project>(arpaContext);

                RuleFor(d => d.StateId)
                    .SelectValueMapping<Command, Project>(arpaContext, a => a.State);

                RuleFor(d => d.GenreId)
                   .SelectValueMapping<Command, Project>(arpaContext, a => a.Genre);

                RuleFor(d => d.TypeId)
                   .SelectValueMapping<Command, Project>(arpaContext, a => a.Type);
            }
        }
    }
}
