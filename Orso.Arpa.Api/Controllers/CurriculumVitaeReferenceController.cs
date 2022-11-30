using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    public class CurriculumVitaeReferencesController : BaseController
    {
        private readonly ICurriculumVitaeReferenceService _curriculumVitaeReferenceService;

        public CurriculumVitaeReferencesController(ICurriculumVitaeReferenceService curriculumVitaeReferenceService)
        {
            _curriculumVitaeReferenceService = curriculumVitaeReferenceService;
        }

        /// <summary>
        /// Gets a curriculum vitae reference by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried curriculumVitaeReference</returns>
        /// <response code="200"></response>
        /// <response code="404">If no curriculum vitae reference could be found for the supplied id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CurriculumVitaeReferenceDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _curriculumVitaeReferenceService.GetByIdAsync(id));
        }

        /// <summary>
        /// Modifies an existing curriculum vitae reference
        /// </summary>
        /// <param name="curriculumVitaeReferenceModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(CurriculumVitaeReferenceModifyDto curriculumVitaeReferenceModifyDto)
        {
            await _curriculumVitaeReferenceService.ModifyAsync(curriculumVitaeReferenceModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing curriculum vitae reference by id
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
            await _curriculumVitaeReferenceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
