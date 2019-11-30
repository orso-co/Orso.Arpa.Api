using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = RoleNames.Orsoadmin)]
        public async Task<ActionResult> Delete(string userName)
        {
            await _userService.DeleteAsync(userName);
            return NoContent();
        }

        [HttpGet("me/profile")]
        public async Task<ActionResult<UserProfileDto>> GetProfileOfCurrentUser()
        {
            return await _userService.GetProfileOfCurrentUserAsync();
        }

        [HttpGet]
        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAsync();
        }
    }
}
