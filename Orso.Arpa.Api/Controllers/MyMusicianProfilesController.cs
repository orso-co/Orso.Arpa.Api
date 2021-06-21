using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.MyMusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/me/profiles/musician")]
    public class MyMusicianProfilesController : BaseController
    {
        private readonly IMeService _meService;

        public MyMusicianProfilesController(IMeService meService)
        {
            _meService = meService;
        }

        /// <summary>
        /// Gets all my musicianProfiles
        /// </summary>
        /// <param name="includeDeactivated">Default: false</param>
        /// <returns>All musicianProfiles of the current user</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [HttpGet]
        [Authorize(Roles = RoleNames.Performer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MyMusicianProfileDto>>> Get([FromQuery] bool includeDeactivated = false)
        {
            return Ok(await _meService.GetMyMusicianProfilesAsync(includeDeactivated));
        }

        /// <summary>
        /// Gets my musicianProfile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Requested musicianProfile of the current user</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [HttpGet("{id}")]
        [Authorize(Roles = RoleNames.Performer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyMusicianProfileDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _meService.GetMyMusicianProfileAsync(id));
        }

        /// <summary>
        /// Adds a new musicianProfile
        /// </summary>
        /// <param name="myMusicianProfileCreateDto"></param>
        /// <returns>The created musicianProfile</returns>
        /// <response code="201">Returns the created musicianProfile</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPost]
        [Authorize(Roles = RoleNames.Performer)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MyMusicianProfileDto>> Post([FromBody] MyMusicianProfileCreateDto myMusicianProfileCreateDto)
        {
            MyMusicianProfileDto createdMusicianProfile = await _meService.CreateMusicianProfileAsync(myMusicianProfileCreateDto);
            return CreatedAtAction(nameof(MusicianProfilesController.GetById), "MusicianProfiles", new { id = createdMusicianProfile.Id }, createdMusicianProfile);
        }

        /// <summary>
        /// Sets the project participation for a musician profile of the current user
        /// </summary>
        /// <param name="myProjectParticipationDto"></param>
        /// <returns>Project participation</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPut("{id}/projects/{projectId}/participation")]
        [Authorize(Roles = RoleNames.Performer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ProjectParticipationDto>> SetProjectParticipation(SetMyProjectParticipationDto myProjectParticipationDto)
        {
            return Ok(await _meService.SetMyProjectParticipationAsync(myProjectParticipationDto));
        }
    }
}
