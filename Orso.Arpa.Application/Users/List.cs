using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.Users.Dtos;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Users
{
    public static class List
    {
        public class Query : IRequest<IEnumerable<UserDto>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<UserDto>>
        {
            private readonly UserManager<User> _userManager;

            public Handler(
                UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IEnumerable<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = _userManager.Users.ToList();

                var dtos = new List<UserDto>();
                foreach (User user in users)
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    dtos.Add(new UserDto
                    {
                        UserName = user.UserName,
                        RoleName = roles.FirstOrDefault(),
                    });
                }

                return dtos;
            }
        }
    }
}
