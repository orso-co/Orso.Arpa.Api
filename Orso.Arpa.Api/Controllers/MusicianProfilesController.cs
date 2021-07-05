using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.ProjectApplication;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/profiles/musicians")]
    public class MusicianProfilesController : BaseController
    {
        private readonly IMusicianProfileService _musicianProfileService;
        private readonly IEducationService _educationService;
        private readonly ICurriculumVitaeReferenceService _curriculumVitaeReferenceService;

        public MusicianProfilesController(IMusicianProfileService musicianProfileService, IEducationService educationService, ICurriculumVitaeReferenceService curriculumVitaeReferenceService)
        {
            _musicianProfileService = musicianProfileService;
            _educationService = educationService;
            _curriculumVitaeReferenceService = curriculumVitaeReferenceService;
        }

        /// <summary>
        /// Gets a musicianProfile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried musicianProfile</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MusicianProfileDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _musicianProfileService.GetByIdAsync(id));
        }

        ///// <summary>
        ///// Deletes existing musicianProfile by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <response code="204"></response>
        ///// <response code="404">If entity could not be found</response>
        ///// <response code="422">If validation fails</response>
        //[Authorize(Roles = RoleNames.Staff)]
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        //public async Task<ActionResult> Delete([FromRoute] Guid id)
        //{
        //    await _musicianProfileService.DeleteAsync(id);

        //    return NoContent();
        //}

        /// <summary>
        /// Gets all project participations of the given musician profile
        /// </summary>
        /// <param name="id">The id of the musician profile</param>
        /// <param name="includeCompleted">Default: false</param>
        /// <returns>All project participations of the given musician profile</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        [Authorize(Roles = RoleNames.PerformerOrStaff)]
        [HttpGet("{id}/projectparticipations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectParticipationDto>>> GetProjectParticipations([FromRoute] Guid id, [FromQuery] bool includeCompleted = false)
        {
            return Ok(await _musicianProfileService.GetProjectParticipationsAsync(id, includeCompleted));
        }

        /// <summary>
        /// Updates the musician profile
        /// </summary>
        /// <returns>The updated musician profile</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<MusicianProfileDto>> Put(MusicianProfileModifyDto musicianProfileModifyDto)
        {
            return Ok(await _musicianProfileService.UpdateAsync(musicianProfileModifyDto));
        }

        /// <summary>
        /// Adds a new education to an existing musician profile
        /// </summary>
        /// <param name="educationCreateDto"></param>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/educations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<EducationDto>> AddEducation(EducationCreateDto educationCreateDto)
        {
            EducationDto createdDto = await _educationService.CreateAsync(educationCreateDto);
            return CreatedAtAction(nameof(EducationsController.GetById), "Educations", new { id = createdDto.Id }, createdDto);
        }

        /// <summary>
        /// Adds a new curriculum vitae reference to an existing musician profile
        /// </summary>
        /// <param name="curriculumVitaeReferenceCreateDto"></param>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("{id}/curriculumvitaereferences")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CurriculumVitaeReferenceDto>> AddCurriculumVitaeReference(CurriculumVitaeReferenceCreateDto curriculumVitaeReferenceCreateDto)
        {
            CurriculumVitaeReferenceDto createdDto = await _curriculumVitaeReferenceService.CreateAsync(curriculumVitaeReferenceCreateDto);
            return CreatedAtAction(nameof(CurriculumVitaeReferencesController.GetById), "CurriculumVitaeReferences", new { id = createdDto.Id }, createdDto);
        }
    }
}
