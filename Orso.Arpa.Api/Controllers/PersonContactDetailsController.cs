using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/persons/{id}/contactdetails")]
    public class PersonContactDetailsController : BaseController
    {
        private readonly IContactDetailService _contactDetailService;

        public PersonContactDetailsController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }

        /// <summary>
        /// Adds new contact details to an existing person
        /// </summary>
        /// <param name="contactDetailCreateDto"></param>
        /// <returns>The created contact detail</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ContactDetailDto>> Post(ContactDetailCreateDto contactDetailCreateDto)
        {
            return Ok(await _contactDetailService.CreateAsync(contactDetailCreateDto));
        }

        /// <summary>
        /// Updates an existing contact detail
        /// </summary>
        /// <param name="contactDetailModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{contactDetailId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(ContactDetailModifyDto contactDetailModifyDto)
        {
            await _contactDetailService.ModifyAsync(contactDetailModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing contact detail
        /// </summary>
        /// <param name="id">the person id</param>
        /// <param name="contactDetailId">the contact detail id</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{contactDetailId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid contactDetailId)
        {
            await _contactDetailService.DeleteAsync(contactDetailId);
            return NoContent();
        }
    }
}
