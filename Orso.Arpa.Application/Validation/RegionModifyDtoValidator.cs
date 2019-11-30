using System;
using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Persistence.Extensions;

namespace Orso.Arpa.Application.Validation
{
    public class RegionModifyDtoValidator : AbstractValidator<RegionModifyDto>
    {
        public RegionModifyDtoValidator(IReadOnlyRepository repository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Id)
                .NotEmpty()
                .MustAsync(async (dto, id, cancellation) => await repository.GetByIdAsync<Region>(id) != null)
                .OnFailure(dto => throw new RestException("Region not found", HttpStatusCode.NotFound, new { Id = "Not found" }));
            RuleFor(c => c.Name)
                .NotEmpty()
                .MustAsync(async (dto, name, cancellation) => !await repository.GetAll<Region>()
                    .ExistsAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                        && r.Id != dto.Id, cancellation))
                .WithMessage("A region with the requested name does already exist");
        }
    }
}
