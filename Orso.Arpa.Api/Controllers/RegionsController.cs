using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Roles;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class RegionsController : BaseController
    {
        private readonly IRegionService _regionService;

        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        /// <summary>
        /// Gets a region by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The queried region</returns>
        /// <response code="200"></response>
        /// <response code="404">If no region could be found for the supplied id</response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegionDto>> GetById([FromRoute] Guid id)
        {
            return await _regionService.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all regions
        /// </summary>
        /// <returns>A list of regions</returns>
        /// <response code="200"></response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegionDto>>> Get([FromQuery] RegionPreferenceType regionPreferenceType)
        {
            return Ok(await _regionService.GetAsync(regionPreferenceType));
        }

        /// <summary>
        /// Creates a new region
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns>The created region</returns>
        /// <response code="201">Returns the created region</response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<RegionDto>> Post([FromBody] RegionCreateDto createDto)
        {
            RegionDto createdDto = await _regionService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        /// <summary>
        /// Modifies existing region
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
        public async Task<IActionResult> Put(RegionModifyDto modifyDto)
        {
            await _regionService.ModifyAsync(modifyDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes existing region by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _regionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
