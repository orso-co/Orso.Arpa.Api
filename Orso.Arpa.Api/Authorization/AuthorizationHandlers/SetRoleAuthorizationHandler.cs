using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Orso.Arpa.Api.Authorization.AuthorizationRequirements;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Api.Authorization.AuthorizationHandlers
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
            SetRole.Command command = null;
            using (var stream = new StreamReader(filterContext.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                string body = stream.ReadToEnd();
                command = JsonConvert.DeserializeObject<SetRole.Command>(body);
            }

            filterContext.HttpContext.Request.Body.Position = 0;

            IEnumerable<string> currentUserRoles = _userAccessor.UserRoles;

            if (currentUserRoles.Contains(RoleNames.Orsoadmin))
            {
                context.Succeed(requirement);
                return;
            }

            User userToEdit = await _userManager.FindByNameAsync(command.UserName);
            IList<string> rolesOfUserToEdit = await _userManager.GetRolesAsync(userToEdit);

            if (rolesOfUserToEdit.Contains(RoleNames.Orsoadmin) || command.RoleName.Equals(RoleNames.Orsoadmin, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Fail();
                return;
            }

            if (currentUserRoles.Contains(RoleNames.Orsonaut))
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
