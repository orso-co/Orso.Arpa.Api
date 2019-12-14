using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PutProfile([FromBody]UserProfileModifyDto userProfileModifyDto)
        {
            await _userService.ModifyProfileOfCurrentUserAsync(userProfileModifyDto);
            return NoContent();
        }
    }
}
