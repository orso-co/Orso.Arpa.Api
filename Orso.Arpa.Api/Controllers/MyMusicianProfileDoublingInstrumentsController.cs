using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/me/profiles/musician/{id}/doublinginstruments")]
    public class MyMusicianProfileDoublingInstrumentsController : BaseController
    {
        private readonly IMeService _meService;

        public MyMusicianProfileDoublingInstrumentsController(IMeService meService)
        {
            _meService = meService;
        }

        /// <summary>
        /// Adds a new doubling instrument to an existing musician profile
        /// </summary>
        /// <param name="myDoublingInstrumentCreateDto"></param>
        /// <returns>The created doubling instrument</returns>
        /// <response code="200">Returns the created doubling instrument</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPost]
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MyDoublingInstrumentDto>> Post(MyDoublingInstrumentCreateDto myDoublingInstrumentCreateDto)
        {
            return Ok(await _meService.CreateDoublingInstrumentAsync(myDoublingInstrumentCreateDto));
        }

        /// <summary>
        /// Updates an existing doubling instrument
        /// </summary>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
        [HttpPut("{doublingInstrumentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(MyDoublingInstrumentModifyDto myDoublingInstrumentModifyDto)
        {
            await _meService.ModifyDoublingInstrumentAsync(myDoublingInstrumentModifyDto);
            return NoContent();
        }
    }
}
