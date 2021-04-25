using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
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
        /// <param name="username"></param>
        /// <response code="204"></response>
        /// <response code="400">If domain validation fails</response>
        /// <response code="422">If formal validation fails</response>
        /// <response code="404">If entity could not be found</response>
        [HttpDelete("{username}")]
        [Authorize(Roles = RoleNames.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] string username)
        {
            await _userService.DeleteAsync(username);
            return NoContent();
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>A list of users</returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAsync();
        }
    }
}
