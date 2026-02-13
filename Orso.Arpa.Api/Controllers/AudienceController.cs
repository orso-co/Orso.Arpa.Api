using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.AudienceApplication.Interfaces;
using Orso.Arpa.Application.AudienceApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class AudienceController : BaseController
    {
        private readonly IAudienceService _audienceService;

        public AudienceController(IAudienceService audienceService)
        {
            _audienceService = audienceService;
        }

        /// <summary>
        /// Searches persons with multiple filters for audience selection
        /// </summary>
        /// <param name="searchDto">Search filters</param>
        /// <returns>Paginated list of matching persons</returns>
        /// <response code="200">Returns the search results</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AudienceSearchResultDto>> Search([FromBody] AudienceSearchDto searchDto)
        {
            return Ok(await _audienceService.SearchAsync(searchDto));
        }
    }
}
