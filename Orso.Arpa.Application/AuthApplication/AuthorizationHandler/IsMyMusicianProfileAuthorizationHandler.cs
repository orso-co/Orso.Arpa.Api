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
    public class IsMyMusicianProfileAuthorizationHandler : AuthorizationHandler<IsMyMusicianProfileRequirement>
    {
        private readonly IArpaContext _arpaContext;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsMyMusicianProfileAuthorizationHandler(
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
            IsMyMusicianProfileRequirement requirement)

        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail(new AuthorizationFailureReason(this, "User is not authenticated"));
                return;
            }

            if (!_httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("id", out object musicianProfileId))
            {
                context.Fail(new AuthorizationFailureReason(this, "No id supplied"));
                return;
            }

            if (context.User.IsInRole(RoleNames.Staff))
            {
                context.Succeed(requirement);
                return;
            }

            bool doesMusicianProfileExists = await _arpaContext.EntityExistsAsync<MusicianProfile>(m =>
                m.Id == Guid.Parse((string)musicianProfileId) && m.PersonId == _tokenAccessor.PersonId, default);

            if (doesMusicianProfileExists)
            {
                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }
    }
}
