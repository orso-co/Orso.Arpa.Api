using FluentValidation;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectCreateDto
    {
        // ToDo: Add properties
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .MaximumLength(1000);
        }
    }
}
