using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/me/profiles/musician")]
    public class MyMusicianProfilesController : BaseController
    {
        private readonly IMeService _meService;
        private readonly IEducationService _educationService;
        private readonly ICurriculumVitaeReferenceService _curriculumVitaeReferenceService;
        public MyMusicianProfilesController(IMeService meService, IEducationService educationService, ICurriculumVitaeReferenceService curriculumVitaeReferenceService)
        {
            _meService = meService;
            _educationService = educationService;
            _curriculumVitaeReferenceService = curriculumVitaeReferenceService;
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
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
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
        /// Updates the musician profile
        /// </summary>
        /// <returns>The updated musician profile</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyMusicianProfileDto>> Put(MyMusicianProfileModifyDto musicianProfileModifyDto)
        {
            return Ok(await _meService.UpdateMusicianProfileAsync(musicianProfileModifyDto));
        }
        /// <summary>
        /// Adds a new education to an existing musician profile
        /// </summary>
        /// <param name="educationCreateDto"></param>
        /// <response code="201"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
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
        [Authorize(Roles = RoleNames.Performer)]
        [Authorize(Policy = AuthorizationPolicies.IsMyMusicianProfile)]
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
