using System;
using FluentValidation;

namespace Orso.Arpa.Application.UrlApplication
{
    public class UrlAddRoleDto
    {
        public Guid roleId { get; set; }
    }

    public class UrlAddRoleDtoValidator : AbstractValidator<UrlAddRoleDto>
    {
        public UrlAddRoleDtoValidator()
        {
            RuleFor(d => d.roleId)
                .NotEmpty();
        }
    }
}
