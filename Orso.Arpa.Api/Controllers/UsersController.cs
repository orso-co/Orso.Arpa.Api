using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Users;
using Orso.Arpa.Application.Users.Dtos;

namespace Orso.Arpa.Api.Controllers
{
    public class UsersController : BaseController
    {
        [HttpDelete("{username}")]
        public async Task<ActionResult<Unit>> Delete(string username)
        {
            return await Mediator.Send(new Delete.Command(username));
        }

        [HttpGet("me/profile")]
        public async Task<ActionResult<UserProfileDto>> GetProfileOfCurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Query());
        }
    }
}
