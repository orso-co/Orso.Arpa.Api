using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.UserDomain.Queries
{
    public static class ListUsers
    {
        public class Query : IRequest<IList<User>>
        {
            public UserStatus? UserStatus { get; set; }

            public Query(UserStatus? userStatus)
            {
                UserStatus = userStatus;
            }
        }

        public class Handler : IRequestHandler<Query, IList<User>>
        {
            private readonly IArpaContext _arpaContext;


            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IList<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<User> users = _arpaContext.Users;

                switch (request.UserStatus)
                {
                    case UserStatus.Active:
                        users = users.Where(u => u.EmailConfirmed && u.UserRoles.Any());
                        break;
                    case UserStatus.AwaitingEmailConfirmation:
                        users = users.Where(u => !u.EmailConfirmed);
                        break;
                    case UserStatus.AwaitingRoleAssignment:
                        users = users.Where(u => u.EmailConfirmed && !u.UserRoles.Any());
                        break;
                    default:
                        break;
                }

                return await users.ToListAsync(cancellationToken);
            }
        }
    }
}
