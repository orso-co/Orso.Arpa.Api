using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/persons/{id}/addresses")]
    public class PersonAddressesController : BaseController
    {
        private readonly IAddressService _addressService;

        public PersonAddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Adds a new address to an existing person
        /// </summary>
        /// <param name="addressCreateDto"></param>
        /// <returns>The created address</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<AddressDto>> Post(AddressCreateDto addressCreateDto)
        {
            return Ok(await _addressService.CreateAsync(addressCreateDto));
        }

        /// <summary>
        /// Updates an existing address
        /// </summary>
        /// <param name="addressModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{addressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(AddressModifyDto addressModifyDto)
        {
            await _addressService.ModifyAsync(addressModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing address
        /// </summary>
        /// <param name="id">the person id</param>
        /// <param name="addressId">the address id</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{addressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid addressId)
        {
            await _addressService.DeleteAsync(addressId);
            return NoContent();
        }
    }
}
