using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization.AuthorizationRequirements;

namespace Orso.Arpa.Application.AuthApplication
{
    public class IsMyPersonAuthorizationHandler : AuthorizationHandler<IsMyPersonRequirement>
    {
        private readonly IArpaContext _arpaContext;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsMyPersonAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor,
            ITokenAccessor tokenAccessor,
            IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
            _tokenAccessor = tokenAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsMyPersonRequirement requirement)

        {
            if (!_httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("id", out object personId))
            {
                context.Fail();
                return;
            }

            if (_tokenAccessor.UserRoles.Contains(RoleNames.Staff))
            {
                context.Succeed(requirement);
                return;
            }

            var personIdAsGuid = Guid.Parse((string)personId);
            if (_tokenAccessor.PersonId.Equals(personIdAsGuid) && await _arpaContext.EntityExistsAsync<Person>(personIdAsGuid, default))
            {
                context.Succeed(requirement);
                return;
            }

            context.Fail();
            return;
        }
    }
}
