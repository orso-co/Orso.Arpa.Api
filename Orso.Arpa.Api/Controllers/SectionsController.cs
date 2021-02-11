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
        [Authorize(Policy = AuthorizationPolicies.AtLeastPerformerPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SectionDto>>> Get()
        {
            return Ok(await _sectionService.GetAsync());
        }
    }
}
