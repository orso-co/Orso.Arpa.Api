using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;

namespace Orso.Arpa.Application.Validation
{
    public class SetRoleDtoValidator : AbstractValidator<SetRoleDto>
    {
        public SetRoleDtoValidator(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.UserName)
                .NotEmpty()
                .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                .OnFailure(_ => throw new RestException("User not found", HttpStatusCode.NotFound, new { user = "Not found" }));
            RuleFor(c => c.RoleName)
                .MustAsync(async (roleName, cancellation) => string.IsNullOrEmpty(roleName) || await roleManager.RoleExistsAsync(roleName))
                .OnFailure(_ => throw new RestException("Role not found", HttpStatusCode.NotFound, new { role = "Not found" }));
        }
    }
}
