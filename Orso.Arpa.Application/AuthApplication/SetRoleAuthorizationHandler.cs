using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;

namespace Orso.Arpa.Application.AuthApplication
{
    public class SetRoleAuthorizationHandler : AuthorizationHandler<SetRoleAuthorizationRequirement>
    {
        private readonly ArpaUserManager _userManager;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SetRoleAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor,
            IUserAccessor userAccessor,
            ArpaUserManager userManager)
        {
            _userManager = userManager;
            _userAccessor = userAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SetRoleAuthorizationRequirement requirement)

        {
            SetRoleDto dto = null;
            Stream body = _httpContextAccessor.HttpContext.Request.Body;

            using (var stream = new StreamReader(body, Encoding.UTF8, true, 1024, true))
            {
                string bodyString = await stream.ReadToEndAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter());
                dto = JsonSerializer.Deserialize<SetRoleDto>(bodyString, options);
            }

            body.Position = 0;

            IList<string> currentUserRoles = _userAccessor.UserRoles;

            if (!currentUserRoles.Any())
            {
                context.Fail();
                return;
            }

            if (currentUserRoles.Contains(RoleNames.Admin))
            {
                context.Succeed(requirement);
                return;
            }

            User userToEdit = await _userManager.FindByNameAsync(dto.Username);
            IList<string> rolesOfUserToEdit = await _userManager.GetRolesAsync(userToEdit);

            if (rolesOfUserToEdit.Contains(RoleNames.Admin)
                || dto.RoleNames.Contains(RoleNames.Admin))
            {
                context.Fail();
                return;
            }

            if (currentUserRoles.Contains(RoleNames.Staff))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
