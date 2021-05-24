using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    public class PersonsController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly IMusicianProfileService _musicianProfileService;

        public PersonsController(IPersonService personService, IMusicianProfileService musicianProfileService)
        {
            _personService = personService;
            _musicianProfileService = musicianProfileService;
        }

        /// <summary>
        /// Gets a person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried person</returns>
        /// <response code="200"></response>
        /// <response code="404">If no person could be found for the supplied id</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonDto>> GetById([FromRoute] Guid id)
        {
            return await _personService.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all persons
        /// </summary>
        /// <returns>A list of persons</returns>
        /// <response code="200"></response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> Get()
        {
            return Ok(await _personService.GetAsync());
        }

        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns>The created person</returns>
        /// <response code="201">Returns the created person</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<PersonDto>> Post([FromBody] PersonCreateDto createDto)
        {
            PersonDto createdDto = await _personService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        /// <summary>
        /// Modifies existing person
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
        public async Task<IActionResult> Put(PersonModifyDto modifyDto)
        {
            await _personService.ModifyAsync(modifyDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes existing person by id
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
            await _personService.DeleteAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Gets all musician profiles for a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDeactivated">Default: false</param>
        /// <returns>The musician profiles of the person</returns>
        /// <response code="201">Returns the created musicianProfile</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}/profiles/musician")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MusicianProfileDto>>> GetMusicianProfiles([FromRoute] Guid id, [FromQuery] bool includeDeactivated = false)
        {
            return Ok(await _musicianProfileService.GetByPersonAsync(id, includeDeactivated));
        }

        /// <summary>
        /// Adds a new musicianProfile
        /// </summary>
        /// <param name="musicianProfileCreateDto"></param>
        /// <returns>The created musicianProfile</returns>
        /// <response code="201">Returns the created musicianProfile</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/profiles/musician")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MusicianProfileDto>> AddMusicianProfile(MusicianProfileCreateDto musicianProfileCreateDto)
        {
            MusicianProfileDto createdMusicianProfile = await _musicianProfileService.CreateAsync(musicianProfileCreateDto);
            return CreatedAtAction(nameof(MusicianProfilesController.GetById), "MusicianProfiles", new { id = createdMusicianProfile.Id }, createdMusicianProfile);
        }
    }
}
