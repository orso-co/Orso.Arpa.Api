using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.MembershipHistoryApplication.Interfaces;
using Orso.Arpa.Application.MembershipHistoryApplication.Model;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/persons/{id}/memberships/{membershipId}/histories")]
    public class MembershipHistoriesController : BaseController
    {
        private readonly IMembershipHistoryService _membershipHistoryService;

        public MembershipHistoriesController(IMembershipHistoryService membershipHistoryService)
        {
            _membershipHistoryService = membershipHistoryService;
        }

        /// <summary>
        /// Gets all history entries for a membership
        /// </summary>
        /// <param name="id">the person id</param>
        /// <param name="membershipId">the membership id</param>
        /// <returns>A list of membership history entries</returns>
        /// <response code="200">Returns the list of history entries</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MembershipHistoryDto>>> Get([FromRoute] Guid id, [FromRoute] Guid membershipId)
        {
            return Ok(await _membershipHistoryService.GetByMembershipAsync(membershipId));
        }

        /// <summary>
        /// Adds a new history entry to an existing membership
        /// </summary>
        /// <param name="membershipHistoryCreateDto"></param>
        /// <returns>The created history entry</returns>
        /// <response code="200">Returns the created history entry</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MembershipHistoryDto>> Post(MembershipHistoryCreateDto membershipHistoryCreateDto)
        {
            return Ok(await _membershipHistoryService.CreateAsync(membershipHistoryCreateDto));
        }

        /// <summary>
        /// Updates an existing history entry
        /// </summary>
        /// <param name="membershipHistoryModifyDto"></param>
        /// <response code="204">Successfully updated</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{historyId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(MembershipHistoryModifyDto membershipHistoryModifyDto)
        {
            await _membershipHistoryService.ModifyAsync(membershipHistoryModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing history entry
        /// </summary>
        /// <param name="id">the person id</param>
        /// <param name="membershipId">the membership id</param>
        /// <param name="historyId">the history entry id</param>
        /// <response code="204">Successfully deleted</response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{historyId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid membershipId, [FromRoute] Guid historyId)
        {
            await _membershipHistoryService.DeleteAsync(historyId);
            return NoContent();
        }
    }
}
