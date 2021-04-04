using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class UrlsController : BaseController
    {
        private readonly IUrlService _urlService;

        public UrlsController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        /// <summary>
        /// Adds an Url to an existing project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlCreateDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or if projectId cannot not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost("{id}/urls")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add([FromRoute] Guid id, [FromBody] UrlCreateDto urlCreateDto)
        {
            await _urlService.AddAsync(id, urlCreateDto);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing Url (of an existing project)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlId"></param>
        /// <param name="urlModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or either project id or urlId cannot be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}/urls/{urlId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromRoute] Guid id, [FromRoute] Guid urlId, [FromBody] UrlModifyDto urlModifyDto)
        {
            await _urlService.PutAsync(id, urlId, urlModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing url by id (from an existing project)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlId"></param>
        /// <response code="204"></response>
        /// <response code="400">If either urlId or projectId cannot not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}/urls/{urlId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid urlId)
        {
            await _urlService.DeleteAsync(id, urlId);
            return NoContent();
        }

        /// <summary>
        /// Adds a role to an existing urlId (from an existing project)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlId"></param>
        /// <param name="urlAddRoleDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If invalid roleId or either urlId or projectId cannot not be found</response>
        // TODO aussagekräftige und detailgenaue Fehlermeldungen
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost("{id}/urls/{urlId}/roles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRole([FromRoute] Guid id, [FromRoute] Guid urlId, [FromBody] UrlAddRoleDto urlAddRoleDto)
        {
            await _urlService.AddRoleAsync(id, urlId, urlAddRoleDto);
            return NoContent();
        }

        /// <summary>
        /// Remove an existing role from an existing urlId (from an existing projectId)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="urlId"></param>
        /// <param name="roleId"></param>
        /// <response code="204"></response>
        /// <response code="400">If either roleId or urlId or projectId cannot not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost("{id}/urls/{urlId}/roles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveRole([FromRoute] Guid id, [FromRoute] Guid urlId, [FromRoute] Guid roleId)
        {
            await _urlService.RemoveRoleAsync(id, urlId, roleId);
            return NoContent();
        }
    }
}
