using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class Create
    {
        public class Command : ICreateCommand<Url>
        {
            public string Href { get; set; }
            public string AnchorText { get; set; }
            public Guid ProjectId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ProjectId)
                    .MustAsync(async (ProjectId, cancellation) => await arpaContext.Projects.AnyAsync(p => p.Id == ProjectId, cancellation))
                    .WithMessage("The project could not be found");
            }
        }
    }
}
