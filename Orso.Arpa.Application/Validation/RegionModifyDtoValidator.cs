using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class RegionModifyDtoValidator : AbstractValidator<RegionModifyDto>
    {
        public RegionModifyDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Id)
                .NotEmpty();
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}
