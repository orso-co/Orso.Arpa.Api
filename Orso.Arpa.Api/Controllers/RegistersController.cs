using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class RegistersController : BaseController
    {
        private readonly ISectionService _registerService;

        public RegistersController(ISectionService registerService)
        {
            _registerService = registerService;
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionDto>>> Get()
        {
            return Ok(await _registerService.GetAsync());
        }
    }
}
