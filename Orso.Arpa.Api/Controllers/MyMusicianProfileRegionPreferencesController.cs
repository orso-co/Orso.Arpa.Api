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
    [Route("api/me/profiles/musician/{id}/regionpreferences")]
    public class MyMusicianProfileRegionPreferencesController : BaseController
    {
        private readonly IMeService _meService;

        public MyMusicianProfileRegionPreferencesController(IMeService meService)
        {
            _meService = meService;
        }

        /// <summary>
        /// Adds a new region preference to an existing musician profile
        /// </summary>
        /// <param name="myRegionPreferenceCreateDto"></param>
        /// <returns>The created region preference</returns>
        /// <response code="200">Returns the created region preference</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPost]
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<RegionPreferenceDto>> Post(MyRegionPreferenceCreateDto myRegionPreferenceCreateDto)
        {
            return Ok(await _meService.CreateRegionPreferenceAsync(myRegionPreferenceCreateDto));
        }

        /// <summary>
        /// Updates an existing region preference
        /// </summary>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
        [HttpPut("{regionPreferenceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(MyRegionPreferenceModifyDto myRegionPreferenceModifyDto)
        {
            await _meService.ModifyRegionPreferenceAsync(myRegionPreferenceModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Removes region preference from existing musician profile
        /// </summary>
        /// <param name="myRegionPreferenceRemoveDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
        [HttpDelete("{regionPreferenceId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RemoveDocument([FromRoute] MyRegionPreferenceRemoveDto myRegionPreferenceRemoveDto)
        {
            await _meService.RemoveRegionPreferenceAsync(myRegionPreferenceRemoveDto);
            return NoContent();
        }
    }
}
