using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/profiles/musicians")]
    public class MusicianProfilesController : BaseController
    {
        private readonly IMusicianProfileService _musicianProfileService;

        public MusicianProfilesController(IMusicianProfileService musicianProfileService)
        {
            _musicianProfileService = musicianProfileService;
        }

        /// <summary>
        /// Gets a musicianProfile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried musicianProfile</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MusicianProfileDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _musicianProfileService.GetByIdAsync(id));
        }


        /// <summary>
        /// Modifies existing musicianProfile
        /// </summary>
        /// <param name="modifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put(MusicianProfileModifyDto modifyDto)
        {
            await _musicianProfileService.ModifyAsync(modifyDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes existing musicianProfile by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _musicianProfileService.DeleteAsync(id);

            return NoContent();
        }
    }
}
