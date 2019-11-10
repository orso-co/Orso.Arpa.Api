using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Users;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Api.Controllers
{
    public class UsersController : BaseController
    {
        [HttpDelete("{username}")]
        [Authorize(Roles = RoleNames.Orsoadmin)]
        public async Task<ActionResult<Unit>> Delete(string username)
        {
            return await Mediator.Send(new Delete.Command(username));
        }

        [HttpGet("me/profile")]
        public async Task<ActionResult<UserProfileDto>> GetProfileOfCurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Query());
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.OrsonautOrsoadmin)]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}
