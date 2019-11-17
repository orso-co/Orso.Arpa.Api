using System;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Extensions;

namespace Orso.Arpa.Application.Validation
{
    public class RegionCreateDtoValidator : AbstractValidator<RegionCreateDto>
    {
        public RegionCreateDtoValidator(IReadOnlyRepository repository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellation) => !await repository.GetAll<Region>()
                    .ExistsAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase), cancellation))
                .WithMessage("Region aleady exists");
        }
    }
}
