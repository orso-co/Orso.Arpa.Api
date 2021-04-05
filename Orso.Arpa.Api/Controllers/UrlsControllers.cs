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
    [Route("api/projects/{id}/urls")]
    public class UrlsController : BaseController
    {
        private readonly IUrlService _urlService;

        public UrlsController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        /// <summary>
        /// Updates an existing Url (of an existing project)
        /// </summary>
        /// <param name="urlModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If dto is not valid or either project id or urlId cannot be found</response>
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
        /// <response code="204"></response>
        /// <response code="400">If either urlId or projectId cannot not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid urlId)
        {
            await _urlService.DeleteAsync(urlId);
            return NoContent();
        }

        /// <summary>
        /// Adds a role to an existing urlId (from an existing project)
        /// </summary>
        /// <param name="addRoleDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If invalid roleId or either urlId or projectId cannot not be found</response>
        // TODO aussagekräftige und detailgenaue Fehlermeldungen
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/roles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRole([FromBodyAndRoute] UrlAddRoleDto addRoleDto)
        {
            await _urlService.AddRoleAsync(addRoleDto);
            return NoContent();
        }

        /// <summary>
        /// Remove an existing role from an existing urlId (from an existing projectId)
        /// </summary>
        /// <param name="removeRoleDto"></param>
        /// <response code="204"></response>
        /// <response code="400">If either roleId or urlId or projectId cannot not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/roles/{roleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveRole([FromRoute] UrlRemoveRoleDto removeRoleDto)
        {
            await _urlService.RemoveRoleAsync(removeRoleDto);
            return NoContent();
        }
    }
}
