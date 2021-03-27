using System;
using System.Collections.Generic;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
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
            public virtual ICollection<Url> Urls { get; private set; } = new HashSet<Url>();
            public Guid? StateId { get; set; }
            public Guid? ParentId { get; set; }
            public bool IsCompleted { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                //RuleFor(d => d.Number)
                //    .EntityDoesNotExist<Command, Project>(arpaContext);

                //    .MustAsync(async (dto, number, cancellation) => !(await arpaContext.Projects
                //        .AnyAsync(ar => ar.number == number, cancellation)))
                //    .WithMessage("The specified project number is already in use. The project number needs to be unique.");
            }
        }

    }
}
