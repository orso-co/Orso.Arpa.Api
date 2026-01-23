using System;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;

namespace Orso.Arpa.Application.SelectValueApplication.Model
{
    /// <summary>
    /// DTO for the request body (without route parameters)
    /// </summary>
    public class SelectValueMappingCreateBodyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Internal DTO combining route parameters with body
    /// </summary>
    public class SelectValueMappingCreateDto
    {
        public string TableName { get; set; }
        public string PropertyName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SelectValueMappingCreateBodyDtoValidator : AbstractValidator<SelectValueMappingCreateBodyDto>
    {
        public SelectValueMappingCreateBodyDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(s => s.Description)
                .RestrictedFreeText(255);
        }
    }

    public class SelectValueMappingCreateDtoValidator : AbstractValidator<SelectValueMappingCreateDto>
    {
        public SelectValueMappingCreateDtoValidator()
        {
            RuleFor(s => s.TableName)
                .NotEmpty();

            RuleFor(s => s.PropertyName)
                .NotEmpty();

            RuleFor(s => s.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(s => s.Description)
                .RestrictedFreeText(255);
        }
    }
}
