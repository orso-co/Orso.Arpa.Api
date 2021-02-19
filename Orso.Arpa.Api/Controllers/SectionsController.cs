using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class SectionsController : BaseController
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        /// <summary>
        /// Gets all sections
        /// </summary>
        /// <returns>A list of sections</returns>
        /// <response code="200"></response>
        [Authorize(Policy = AuthorizationPolicies.HasRolePolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SectionDto>>> Get()
        {
            return Ok(await _sectionService.GetAsync());
        }

        /// <summary>
        /// Gets all sections as tree hierarchy
        /// </summary>
        /// <returns>A tree of sections</returns>
        /// <response code="200"></response>
        [AllowAnonymous]
        [HttpGet("tree")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SectionTreeDto>> GetTree([FromQuery] int? maxLevel = null)
        {
            return Ok(await _sectionService.GetTreeAsync(maxLevel));
        }
    }
}
