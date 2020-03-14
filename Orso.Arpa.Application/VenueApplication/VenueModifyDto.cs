using System;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Application.VenueApplication
{
    public class VenueModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

        // ToDo: Add properties
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class VenueModifyDtoValidator : AbstractValidator<VenueModifyDto>
    {
        public VenueModifyDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(v => v.Description)
                .MaximumLength(255);
        }
    }
}
