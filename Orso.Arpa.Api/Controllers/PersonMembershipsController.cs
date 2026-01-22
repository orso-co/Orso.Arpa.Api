using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.PersonMembershipApplication.Interfaces;
using Orso.Arpa.Application.PersonMembershipApplication.Model;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/persons/{id}/memberships")]
    public class PersonMembershipsController : BaseController
    {
        private readonly IPersonMembershipService _personMembershipService;

        public PersonMembershipsController(IPersonMembershipService personMembershipService)
        {
            _personMembershipService = personMembershipService;
        }

        /// <summary>
        /// Gets all memberships for a person
        /// </summary>
        /// <param name="id">the person id</param>
        /// <returns>A list of person memberships</returns>
        /// <response code="200">Returns the list of memberships</response>
        /// <response code="404">If the person could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PersonMembershipDto>>> Get([FromRoute] Guid id)
        {
            return Ok(await _personMembershipService.GetByPersonAsync(id));
        }

        /// <summary>
        /// Adds a new membership to an existing person
        /// </summary>
        /// <param name="personMembershipCreateDto"></param>
        /// <returns>The created membership</returns>
        /// <response code="200">Returns the created membership</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<PersonMembershipDto>> Post(PersonMembershipCreateDto personMembershipCreateDto)
        {
            return Ok(await _personMembershipService.CreateAsync(personMembershipCreateDto));
        }

        /// <summary>
        /// Updates an existing membership
        /// </summary>
        /// <param name="personMembershipModifyDto"></param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{membershipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(PersonMembershipModifyDto personMembershipModifyDto)
        {
            await _personMembershipService.ModifyAsync(personMembershipModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing membership
        /// </summary>
        /// <param name="id">the person id</param>
        /// <param name="membershipId">the membership id</param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{membershipId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid membershipId)
        {
            await _personMembershipService.DeleteAsync(membershipId);
            return NoContent();
        }
    }
}
