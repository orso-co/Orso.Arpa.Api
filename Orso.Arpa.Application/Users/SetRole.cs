using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Application.Users
{
    public static class SetRole
    {
        public class Command : IRequest
        {
            public Command(string username, string roleName)
            {
                Username = username;
                RoleName = roleName;
            }
            public string Username { get; }
            public string RoleName { get; }
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
                User user = await _userManager.FindByNameAsync(request.Username);

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
