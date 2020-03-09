using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Infrastructure.Authorization;

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

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectValueDto>>> Get([FromRoute]string tableName, [FromRoute]string propertyName)
        {
            return Ok(await _selectValueService.GetAsync(tableName, propertyName));
        }
    }
}
