using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;

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

        /// <summary>
        /// Adds a stakeholder group to an existing person
        /// </summary>
        /// <param name="addStakeholderGroupDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.IsMyPerson)]
        [HttpPost("{id}/stakeholdergroups/{stakeholderGroupId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> AddStakeholderGroup([FromRoute] PersonAddStakeholderGroupDto addStakeholderGroupDto)
        {
            await _personService.AddStakeholderGroupAsync(addStakeholderGroupDto);
            return NoContent();
        }

        /// <summary>
        /// Removes stakeholder group from existing person
        /// </summary>
        /// <param name="removeStakeholderGroupDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.IsMyPerson)]
        [HttpDelete("{id}/stakeholdergroups/{stakeholderGroupId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RemoveStakeholderGroup([FromRoute] PersonRemoveStakeholderGroupDto removeStakeholderGroupDto)
        {
            await _personService.RemoveStakeholderGroupAsync(removeStakeholderGroupDto);
            return NoContent();
        }

        /// <summary>
        /// Invites persons to ARPA by email
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>A list of person ids to be invited to ARPA</returns>
        /// <response code="200">Returns result dto</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("invite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<PersonInviteResultDto>> Invite([FromBody] PersonInviteDto dto)
        {
            return Ok(await _personService.InviteAsync(dto));
        }

        /// <summary>
        /// Sets the profile picture for the given person
        /// </summary>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.IsMyPerson)]
        [HttpPost("{id}/profilepicture")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SetProfilePicture(ProfilePictureCreateDto profilePictureCreateDto)
        {
            IFileResult fileResult = await _personService.SetProfilePictureAsync(profilePictureCreateDto);
            return CreatedAtAction(
                nameof(GetProfilePicture),
                new { id = profilePictureCreateDto.Id.ToString() },
                File(fileResult.Content, fileResult.ContentType, $"Arpa_Profile_Picture_{profilePictureCreateDto.Id}{fileResult.Extension}"));
        }

        /// <summary>
        /// Gets profile picture for the given person
        /// </summary>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpGet("{id}/profilepicture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetProfilePicture([FromRoute] Guid id)
        {
            // This implementation is only reached when ArpaProfilePictureProvider.IsValidRequest returns false

            IFileResult fileResult = await _personService.GetProfilePictureAsync(id);

            return File(fileResult.Content, fileResult.ContentType, $"Arpa_Profile_Picture_{id}{fileResult.Extension}");
        }

        /// <summary>
        /// Deletes the profile picture for the given person
        /// </summary>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.IsMyPerson)]
        [HttpDelete("{id}/profilepicture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProfilePicture([FromRoute] Guid id)
        {
            await _personService.DeleteProfilePictureAsync(id);
            return NoContent();
        }
    }
}
