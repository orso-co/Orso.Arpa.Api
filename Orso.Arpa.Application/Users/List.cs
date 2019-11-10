using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            private readonly IMapper _mapper;

            public Handler(
                UserManager<User> userManager,
                IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<IEnumerable<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = _userManager.Users.ToList();

                var dtos = new List<UserDto>();
                foreach (User user in users)
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    UserDto dto = _mapper.Map<UserDto>(user);
                    dto.RoleName = roles.FirstOrDefault();
                    dtos.Add(dto);
                }

                return dtos;
            }
        }
    }
}
