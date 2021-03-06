using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Orso.Arpa.Application;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Resources.Cultures;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class Create
    {
        public class Command : ICreateCommand<Region>
        {
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, IStringLocalizer<DomainResource>  localizer)
            {


                RuleFor(c => c.Name)
                    .MustAsync(async (name, cancellation) => !(await arpaContext.Regions
                        .AnyAsync(r => r.Name == name, cancellation)))
                    .WithMessage(localizer["A region with the requested name does already exist"]);
            }
        }
    }
}
