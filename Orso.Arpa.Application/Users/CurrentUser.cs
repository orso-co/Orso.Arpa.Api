using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Users.Dtos;

namespace Orso.Arpa.Application.Users
{
    public static class CurrentUser
    {
        public class Query : IRequest<UserProfileDto> { }

        public class Handler : IRequestHandler<Query, UserProfileDto>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(
                IUserAccessor userAccessor,
                IMapper mapper)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<UserProfileDto> Handle(Query request, CancellationToken cancellationToken)
            {
                return _mapper.Map<UserProfileDto>(await _userAccessor.GetCurrentUserAsync());
            }
        }
    }
}
