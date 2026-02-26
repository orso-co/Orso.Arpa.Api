using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Api.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IArpaContext _arpaContext;

        public SearchController(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        /// <summary>
        /// Global search across Persons, Appointments, Projects, and News.
        /// Available to all authenticated users (not just Staff).
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GlobalSearchResult>>> Search(
            [FromQuery] string query = "",
            [FromQuery] int maxResults = 50)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Trim().Length < 2)
            {
                return Ok(new List<GlobalSearchResult>());
            }

            if (maxResults > 100) maxResults = 100;
            if (maxResults < 1) maxResults = 1;

            var results = await _arpaContext.GlobalSearchAsync(query.Trim(), maxResults);
            return Ok(results);
        }
    }
}
