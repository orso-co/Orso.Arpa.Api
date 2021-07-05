using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AppointmentApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    public class EducationsController : BaseController
    {
        private readonly IEducationService _educationService;

        public EducationsController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        /// <summary>
        /// Gets an education by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried education</returns>
        /// <response code="200"></response>
        /// <response code="404">If no education could be found for the supplied id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppointmentDto>> GetById([FromRoute] Guid id)
        {
            return Ok(await _educationService.GetByIdAsync(id));
        }

        /// <summary>
        /// Modifies an existing education
        /// </summary>
        /// <param name="educationModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(EducationModifyDto educationModifyDto)
        {
            await _educationService.ModifyAsync(educationModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing education by id 
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
            await _educationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
