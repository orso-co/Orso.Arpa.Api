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
    [Route("api/profiles/musicians/{id}/doublinginstruments")]
    public class MusicianProfileDoublingInstrumentsController : BaseController
    {
        private readonly IDoublingInstrumentService _doublingInstrumentService;

        public MusicianProfileDoublingInstrumentsController(IDoublingInstrumentService doublingInstrumentService)
        {
            _doublingInstrumentService = doublingInstrumentService;
        }

        /// <summary>
        /// Adds a new doubling instrument to an existing musician profile
        /// </summary>
        /// <param name="doublingInstrumentCreateDto"></param>
        /// <returns>The created doubling instrument</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<DoublingInstrumentDto>> Post(DoublingInstrumentCreateDto doublingInstrumentCreateDto)
        {
            return Ok(await _doublingInstrumentService.CreateAsync(doublingInstrumentCreateDto));
        }

        /// <summary>
        /// Updates an existing doubling instrument
        /// </summary>
        /// <param name="doublingInstrumentModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{doublingInstrumentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(DoublingInstrumentModifyDto doublingInstrumentModifyDto)
        {
            await _doublingInstrumentService.ModifyAsync(doublingInstrumentModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing doubling instrument
        /// </summary>
        /// <param name="id">the musician profile id</param>
        /// <param name="doublingInstrumentId">the doubling instrument id</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{doublingInstrumentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid doublingInstrumentId)
        {
            await _doublingInstrumentService.DeleteAsync(doublingInstrumentId);
            return NoContent();
        }
    }
}
