using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Users
{
    public static class Delete
    {
        public class Command : IRequest
        {
            public Command(string username)
            {
                UserName = username;
            }
            public string UserName { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    throw new RestException("User not found", HttpStatusCode.NotFound, new { user = "Not found" });
                }

                user.Delete();
                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }
                throw new IdentityException("Problem deleting user", result.Errors);
            }
        }

    }
}
