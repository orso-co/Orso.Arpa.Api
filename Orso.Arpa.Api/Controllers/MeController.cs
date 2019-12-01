using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/users/me")]
    public class MeController : BaseController
    {
        private readonly IUserService _userService;

        public MeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileDto>> GetMyProfile()
        {
            return await _userService.GetProfileOfCurrentUserAsync();
        }
    }
}
