using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.ProjectDomain.Commands
{
    public static class ModifyProject
    {
        public class Command : IModifyCommand<Project>
        {
            public Guid Id { get; set; }

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
            public bool IsHiddenForPerformers { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .EntityExists<Command, Project>(arpaContext);

#pragma warning disable RCS1155 // Use StringComparison when comparing strings. (this won't work with ef core server side query execution)
                _ = RuleFor(d => d.Code)
                    .MustAsync(async (dto, code, cancellation) => !await arpaContext.Projects
                        .AnyAsync(project => dto.Id != project.Id && project.Code.ToLower() == code.ToLower(), cancellation))
                    .WithMessage("The specified project code is already in use. The project code needs to be unique.");
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.

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
