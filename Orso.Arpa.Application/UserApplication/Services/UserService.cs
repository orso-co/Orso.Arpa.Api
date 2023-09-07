using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.UserApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Users;

namespace Orso.Arpa.Application.Services
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
            IEnumerable<User> users = await _mediator.Send(new List.Query());

            var dtos = new List<UserDto>();
            foreach (User user in users)
            {
                UserDto dto = _mapper.Map<UserDto>(user);
                dto.RoleNames = await _mediator.Send(new UserRoles.Query(user));
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<UserDto>(await _mediator.Send(new Details.Query(id)));
        }

        public async Task DeleteAsync(string userName)
        {
            await _mediator.Send(new Delete.Command(userName));
        }
    }
}
