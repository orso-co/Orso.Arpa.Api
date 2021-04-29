using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Identity;

namespace Orso.Arpa.Domain.Logic.Users
{
    public static class Delete
    {
        public class Command : IRequest
        {
            public Command(string userName)
            {
                UserName = userName;
            }

            public string UserName { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;

            public Handler(ArpaUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), nameof(Command.UserName));
                }

                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }
                throw new IdentityException("Problem deleting user", result.Errors);
            }
        }
    }
}
