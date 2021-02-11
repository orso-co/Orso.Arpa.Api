using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.Middleware;
using Orso.Arpa.Api.ModelBinding;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RegionApplication;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetById([FromRoute] Guid id)
        {
            return await _regionService.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all regions
        /// </summary>
        /// <returns>A list of regions</returns>
        /// <response code="200"></response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RegionDto>>> Get()
        {
            return Ok(await _regionService.GetAsync());
        }

        /// <summary>
        /// Creates a new region
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns>The created region</returns>
        /// <response code="201">Returns the created region</response>
        /// <response code="400">If dto is not valid</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
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
        /// <response code="400">If dto is not valid or if region could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        [SwaggerFromRouteProperty(nameof(RegionModifyDto.Id))]
        public async Task<IActionResult> Put([FromBodyAndRoute] RegionModifyDto modifyDto)
        {
            await _regionService.ModifyAsync(modifyDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes existing region by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"></response>
        /// <response code="400">If region could not be found</response>
        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _regionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
