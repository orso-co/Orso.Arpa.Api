using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.UserDomain.Commands
{
    public class SignOutUser
    {
        public class Command : IRequest<bool>
        {
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ICookieSignIn _cookieSignIn;


            public Handler(
                ICookieSignIn cookieSignInService)
            {
                _cookieSignIn = cookieSignInService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                Task signOutTask = _cookieSignIn.SignOutUser();
                await signOutTask;

                return signOutTask.IsCompletedSuccessfully;
            }

        }
    }
}