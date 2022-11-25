using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
                await _userManager.DeleteAsync(request.UserName);
                return Unit.Value;
            }
        }
    }
}
