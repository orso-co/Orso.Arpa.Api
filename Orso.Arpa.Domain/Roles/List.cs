using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.Roles
{
    public static class List
    {
        public class Query : IRequest<IEnumerable<Role>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<Role>>
        {
            private readonly RoleManager<Role> _roleManager;

            public Handler(
                RoleManager<Role> roleManager)
            {
                _roleManager = roleManager;
            }

            public Task<IEnumerable<Role>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_roleManager.Roles.ToList() as IEnumerable<Role>);
            }
        }
    }
}
