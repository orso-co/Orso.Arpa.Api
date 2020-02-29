using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.Logic.Me;
using Orso.Arpa.Application.Logic.Users;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Logic.Users;

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
                Domain.Entities.Role role = await _mediator.Send(new Domain.Logic.Users.Role.Query(user));
                UserDto dto = _mapper.Map<UserDto>(user);

                if (role != null)
                {
                    dto.RoleName = role.Name;
                    dto.RoleLevel = role.Level;
                }

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
            User user = await _mediator.Send(new Details.Query());
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task ModifyProfileOfCurrentUserAsync(Logic.Me.Modify.Dto userProfileModifyDto)
        {
            Domain.Logic.Me.Modify.Command command = _mapper.Map<Domain.Logic.Me.Modify.Command>(userProfileModifyDto);
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<UserAppointmentDto>> GetAppointmentsOfCurrentUserAsync(int? limit, int? offset)
        {
            IEnumerable<Appointment> appointments = await _mediator.Send(new Appointments.Query(limit, offset));
            return _mapper.Map<IEnumerable<UserAppointmentDto>>(appointments);
        }
    }
}
