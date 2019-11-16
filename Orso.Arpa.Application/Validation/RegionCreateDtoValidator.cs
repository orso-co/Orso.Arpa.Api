using System;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Persistence.DataAccess;
using Orso.Arpa.Persistence.Extensions;

namespace Orso.Arpa.Application.Validation
{
    public class RegionCreateDtoValidator : AbstractValidator<RegionCreateDto>
    {
        public RegionCreateDtoValidator(ArpaContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellation) => !await context.Regions
                    .ExistsAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase), cancellation))
                .WithMessage("Region aleady exists");
        }
    }
}
