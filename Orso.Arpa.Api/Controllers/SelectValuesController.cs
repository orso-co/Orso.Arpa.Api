using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.SelectValueApplication.Interfaces;
using Orso.Arpa.Application.SelectValueApplication.Model;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/tables/{tableName}/properties/{propertyName}")]
    public class SelectValuesController : BaseController
    {
        private readonly ISelectValueService _selectValueService;

        public SelectValuesController(ISelectValueService selectValueService)
        {
            _selectValueService = selectValueService;
        }

        /// <summary>
        /// Queries select values by table name and property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="tableName"></param>
        /// <returns>A list of select values</returns>
        /// <response code="200"></response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SelectValueDto>>> Get([FromRoute] string tableName, [FromRoute] string propertyName)
        {
            return Ok(await _selectValueService.GetAsync(tableName, propertyName));
        }
    }
}
