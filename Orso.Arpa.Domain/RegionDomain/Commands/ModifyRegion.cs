using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.RegionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.RegionDomain.Commands
{
    public static class ModifyRegion
    {
        public class Command : IModifyCommand<Region>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool IsForPerformance { get; set; }
            public bool IsForRehearsal { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Region>(arpaContext);

                RuleFor(c => c.Name)
                    .MustAsync(async (dto, name, cancellation) => !await arpaContext
#pragma warning disable RCS1155, CA1862 // Use StringComparison when comparing strings.
                        .EntityExistsAsync<Region>(r => r.Name.ToLower() == name.ToLower() && r.Id != dto.Id, cancellation))
#pragma warning restore RCS1155, CA1862 // Use StringComparison when comparing strings.
                    .WithMessage("A region with the requested name does already exist");
            }
        }
    }
}
