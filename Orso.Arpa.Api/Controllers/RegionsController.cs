using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.ModelBinders;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Regions;
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

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetById(Guid id)
        {
            return await _regionService.GetByIdAsync(id);
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> Get()
        {
            return Ok(await _regionService.GetAsync());
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RegionDto>> Post([FromBody]Create.Dto createDto)
        {
            RegionDto createdDto = await _regionService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(
            [FromBody][ModelBinder(typeof(ModifyDtoModelBinder<Modify.Dto>))]Modify.Dto modifyDto)
        {
            await _regionService.ModifyAsync(modifyDto);

            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsonautPolicy)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _regionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
