using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Users;

namespace Orso.Arpa.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserService(IMediator mediator, IMapper mapper)
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
                IEnumerable<string> roles = await _mediator.Send(new Roles.Query(user));
                UserDto dto = _mapper.Map<UserDto>(user);
                dto.RoleName = roles.FirstOrDefault();
                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task DeleteAsync(string userName)
        {
            await _mediator.Send(new Delete.Command(userName));
        }

        public async Task<UserProfileDto> GetProfileOfCurrentUserAsync()
        {
            User user = await _mediator.Send(new CurrentUser.Query());
            return _mapper.Map<UserProfileDto>(user);
        }
    }
}
