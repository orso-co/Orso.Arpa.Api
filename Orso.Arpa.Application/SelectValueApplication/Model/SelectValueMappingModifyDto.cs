using System;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;

namespace Orso.Arpa.Application.SelectValueApplication.Model
{
    public class SelectValueMappingModifyDto : IdFromRouteDto<SelectValueMappingModifyBodyDto>
    {
        public string TableName { get; set; }
        public string PropertyName { get; set; }
    }

    public class SelectValueMappingModifyBodyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SelectValueMappingModifyDtoValidator : IdFromRouteDtoValidator<SelectValueMappingModifyDto, SelectValueMappingModifyBodyDto>
    {
        public SelectValueMappingModifyDtoValidator()
        {
            RuleFor(d => d.TableName)
                .NotEmpty();

            RuleFor(d => d.PropertyName)
                .NotEmpty();

            RuleFor(d => d.Body)
                .SetValidator(new SelectValueMappingModifyBodyDtoValidator());
        }
    }

    public class SelectValueMappingModifyBodyDtoValidator : AbstractValidator<SelectValueMappingModifyBodyDto>
    {
        public SelectValueMappingModifyBodyDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .PlaceName(50);

            RuleFor(s => s.Description)
                .RestrictedFreeText(255);
        }
    }
}
