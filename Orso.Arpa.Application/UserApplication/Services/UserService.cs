using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.UserApplication.Interfaces;
using Orso.Arpa.Application.UserApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Queries;

namespace Orso.Arpa.Application.UserApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserService(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAsync(UserStatus? userStatus)
        {
            IList<User> users = await _mediator.Send(new ListUsers.Query(userStatus));
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            User user = await _mediator.Send(new GetUserById.Query(id));
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByPersonIdAsync(Guid personId)
        {
            User user = await _mediator.Send(new GetUserByPersonId.Query(personId));
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(string userName)
        {
            await _mediator.Send(new DeleteUser.Command(userName));
        }
    }
}
