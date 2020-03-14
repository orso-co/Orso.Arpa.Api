using System;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

        // ToDo: Add Properties
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class ProjectModifyDtoValidator : AbstractValidator<ProjectModifyDto>
    {
        public ProjectModifyDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .MaximumLength(1000);
        }
    }
}
