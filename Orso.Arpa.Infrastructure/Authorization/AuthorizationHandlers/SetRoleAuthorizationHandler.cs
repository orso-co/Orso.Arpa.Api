using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;

namespace Orso.Arpa.Infrastructure.Authorization.AuthorizationHandlers
{
    public class SetRoleAuthorizationHandler : AuthorizationHandler<SetRoleAuthorizationRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SetRoleAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor,
            IUserAccessor userAccessor,
            UserManager<User> userManager)
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
                dto = JsonConvert.DeserializeObject<SetRoleDto>(bodyString);
            }

            body.Position = 0;

            string currentUserRole = _userAccessor.UserRole;

            if (currentUserRole.Equals(RoleNames.Orsoadmin, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Succeed(requirement);
                return;
            }

            User userToEdit = await _userManager.FindByNameAsync(dto.UserName);
            IList<string> rolesOfUserToEdit = await _userManager.GetRolesAsync(userToEdit);

            if (rolesOfUserToEdit.Contains(RoleNames.Orsoadmin)
                || dto.RoleName.Equals(RoleNames.Orsoadmin, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Fail();
                return;
            }

            if (currentUserRole.Equals(RoleNames.Orsonaut, StringComparison.InvariantCultureIgnoreCase))
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
