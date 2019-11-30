using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public SetRoleAuthorizationHandler(IUserAccessor userAccessor, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userAccessor = userAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SetRoleAuthorizationRequirement requirement)

        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            SetRoleDto dto = null;
            using (var stream = new StreamReader(filterContext.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                string body = stream.ReadToEnd();
                dto = JsonConvert.DeserializeObject<SetRoleDto>(body);
            }

            filterContext.HttpContext.Request.Body.Position = 0;

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
