using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.UserApplication.Interfaces;
using Orso.Arpa.Application.UserApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
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

        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            IEnumerable<User> users = await _mediator.Send(new ListUsers.Query());

            var dtos = new List<UserDto>();
            foreach (User user in users)
            {
                UserDto dto = _mapper.Map<UserDto>(user);
                dto.RoleNames = await _mediator.Send(new ListUserRoles.Query(user));
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<UserDto>(await _mediator.Send(new GetUser.Query(id)));
        }

        public async Task DeleteAsync(string userName)
        {
            await _mediator.Send(new DeleteUser.Command(userName));
        }
    }
}
