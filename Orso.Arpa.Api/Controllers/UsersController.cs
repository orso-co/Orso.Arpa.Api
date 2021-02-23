using System;
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
        /// <param name="username"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{username}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
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
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>The queried user</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<UserDto> GetById([FromRoute] Guid id)
        {
            return await _userService.GetByIdAsync(id);
        }
    }
}