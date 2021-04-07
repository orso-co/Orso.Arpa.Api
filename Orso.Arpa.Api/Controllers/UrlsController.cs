using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Domain.Roles;

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
        /// Gets an url by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried appointment</returns>
        /// <response code="200"></response>
        /// <response code="404">If no url could be found for the supplied id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _urlService.GetByIdAsync(id));
        }

        /// <summary>
        /// Modifies an existing Url (of an existing project)
        /// </summary>
        /// <param name="urlModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or urlId cannot be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromBodyAndRoute] UrlModifyDto urlModifyDto)
        {
            await _urlService.PutAsync(urlModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing url by id (from an existing project)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="400">If either urlId cannot not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _urlService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Adds a role to an existing urlId (from an existing project)
        /// </summary>
        /// <param name="addRoleDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If invalid roleId or either urlId cannot not be found</response>
        // TODO aussagekräftige und detailgenaue Fehlermeldungen
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UrlDto>> AddRole([FromBodyAndRoute] UrlAddRoleDto addRoleDto)
        {
            return await _urlService.AddRoleAsync(addRoleDto);
        }

        /// <summary>
        /// Remove an existing role from an existing urlId (from an existing projectId)
        /// </summary>
        /// <param name="removeRoleDto"></param>
        /// <response code="200"></response>
        /// <response code="400">If either roleId or urlId cannot not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}/roles/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UrlDto>> RemoveRole([FromRoute] UrlRemoveRoleDto removeRoleDto)
        {
            return await _urlService.RemoveRoleAsync(removeRoleDto);
        }
    }
}
