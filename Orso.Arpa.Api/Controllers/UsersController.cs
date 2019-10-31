using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Users;

namespace Orso.Arpa.Api.Controllers
{
    public class UsersController : BaseController
    {
        [HttpDelete("{username}")]
        public async Task<ActionResult<Unit>> Delete(string username)
        {
            return await Mediator.Send(new Delete.Command(username));
        }
    }
}
