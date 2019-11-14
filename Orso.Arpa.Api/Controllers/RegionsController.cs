using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Api.ModelBinders;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    public class RegionsController : BaseController
    {
        private readonly IRegionService _regionService;

        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetById(Guid id)
        {
            return await _regionService.GetByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> Get()
        {
            return await _regionService.GetAsync();
        }

        [Authorize(Roles = RoleNames.OrsonautOrsoadmin)]
        [HttpPost]
        public async Task<ActionResult<RegionDto>> Post([FromBody]RegionCreateDto createDto)
        {
            RegionDto createdDto = await _regionService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
        }

        [Authorize(Roles = RoleNames.OrsonautOrsoadmin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [FromBody][ModelBinder(typeof(ModifyDtoModelBinder<RegionModifyDto>))]RegionModifyDto modifyDto)
        {
            await _regionService.ModifyAsync(modifyDto);

            return NoContent();
        }

        [Authorize(Roles = RoleNames.OrsonautOrsoadmin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _regionService.DeleteAsync(id);

            return NoContent();
        }
    }
}
