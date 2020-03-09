using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Infrastructure.Authorization;

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
        public async Task<ActionResult<MyProfileDto>> GetMyProfile()
        {
            return await _userService.GetProfileOfCurrentUserAsync();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpGet("appointments")]
        public async Task<ActionResult<MyAppointmentListDto>> GetMyAppointments(
            [FromQuery]int? limit,
            [FromQuery]int? offset)
        {
            return Ok(await _userService.GetAppointmentsOfCurrentUserAsync(limit, offset));
        }

        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PutProfile([FromBody]MyProfileModifyDto userProfileModifyDto)
        {
            await _userService.ModifyProfileOfCurrentUserAsync(userProfileModifyDto);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastOrsianerPolicy)]
        [HttpPut("appointments/{id}/participation/prediction/{predictionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> SetParticipationPrediction([FromRoute]SetMyProjectAppointmentPredictionDto setParticipationPrediction)
        {
            await _userService.SetAppointmentParticipationPredictionAsync(setParticipationPrediction);
            return NoContent();
        }
    }
}
