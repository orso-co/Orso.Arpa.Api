using FluentValidation;

namespace Orso.Arpa.Application.SelectValueApplication
{
    public class SelectValueCreateDto
    {
        // ToDo: Add properties
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SelectValueCreateDtoValidator : AbstractValidator<SelectValueCreateDto>
    {
        public SelectValueCreateDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(s => s.Description)
                .MaximumLength(255);
        }
    }
}
