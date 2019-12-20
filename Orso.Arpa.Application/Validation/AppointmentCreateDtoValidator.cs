using FluentValidation;
using Orso.Arpa.Application.Dtos;

namespace Orso.Arpa.Application.Validation
{
    public class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
    {
        public AppointmentCreateDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.CategoryId)
                .NotEmpty();
            RuleFor(d => d.StatusId)
                .NotEmpty();
            RuleFor(d => d.StartTime)
                .NotEmpty();
            RuleFor(d => d.EndTime)
                .NotEmpty()
                .Must((dto, endTime) => endTime >= dto.StartTime)
                .WithMessage("EndTime must be greater than StartTime");
            RuleFor(d => d.Name)
                .NotEmpty();
            RuleFor(d => d.EmolumentId)
                .NotEmpty();
        }
    }
}
