using System;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.SelectValueApplication
{
    public class SelectValueModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

        // ToDo: Add properties
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SelectValueModifyDtoValidator : AbstractValidator<SelectValueModifyDto>
    {
        public SelectValueModifyDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(s => s.Description)
                .MaximumLength(255);
        }
    }
}
