using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.UserApplication;
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

        /// <summary>
        /// Deletes existing user by username. Does not delete underlying person!
        /// </summary>
        /// <param name="userName"></param>
        /// <response code="204"></response>
        /// <response code="404">If user could not be found</response>
        [HttpDelete("{username}")]
        [Authorize(Roles = RoleNames.Orsoadmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete([FromRoute] string userName)
        {
            await _userService.DeleteAsync(userName);
            return NoContent();
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>A list of users</returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAsync();
        }
    }
}
