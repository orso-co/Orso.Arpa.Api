using System;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.SectionApplication
{
    public class SectionModifyDto : IModifyDto
    {
        // ToDo: Add properties

        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class SectionModifyDtoValidator : AbstractValidator<SectionModifyDto>
    {
        public SectionModifyDtoValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty();

            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
