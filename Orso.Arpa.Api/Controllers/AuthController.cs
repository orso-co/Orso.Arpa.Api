using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Auth;

namespace Orso.Arpa.Api.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}
