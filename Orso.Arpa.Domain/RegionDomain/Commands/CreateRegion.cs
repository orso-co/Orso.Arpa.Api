using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.RegionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.RegionDomain.Commands
{
    public static class CreateRegion
    {
        public class Command : ICreateCommand<Region>
        {
            public string Name { get; set; }
            public bool IsForRehearsal { get; set; }
            public bool IsForPerformance { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Name)
                    .MustAsync(async (name, cancellation) => !(await arpaContext.Regions
                        .AnyAsync(r => r.Name == name, cancellation)))
                    .WithMessage("A region with the requested name does already exist");
            }
        }
    }
}
