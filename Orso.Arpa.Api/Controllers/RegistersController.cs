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
        private readonly IRegisterService _registerService;

        public RegistersController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterDto>>> Get()
        {
            return Ok(await _registerService.GetAsync());
        }
    }
}
