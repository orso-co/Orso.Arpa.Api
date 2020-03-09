using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Application.UserApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.Me;
using Orso.Arpa.Domain.Logic.Users;
using AppointmentParticipations = Orso.Arpa.Domain.Logic.AppointmentParticipations;

namespace Orso.Arpa.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public UserService(
            IMediator mediator,
            IMapper mapper,
            IUserAccessor userAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userAccessor = userAccessor;
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

        public async Task<MyProfileDto> GetProfileOfCurrentUserAsync()
        {
            User user = await _mediator.Send(new Details.Query());
            return _mapper.Map<MyProfileDto>(user);
        }

        public async Task ModifyProfileOfCurrentUserAsync(MyProfileModifyDto userProfileModifyDto)
        {
            Modify.Command command = _mapper.Map<Modify.Command>(userProfileModifyDto);
            await _mediator.Send(command);
        }

        public async Task<MyAppointmentListDto> GetAppointmentsOfCurrentUserAsync(int? limit, int? offset)
        {
            Tuple<IEnumerable<Appointment>, int> appointmentTuple = await _mediator.Send(new AppointmentList.Query(limit, offset));
            MyAppointmentListDto listDto = _mapper.Map<MyAppointmentListDto>(appointmentTuple);

            User currentUser = await _userAccessor.GetCurrentUserAsync();

            foreach (MyAppointmentDto dto in listDto.UserAppointments)
            {
                AppointmentParticipation participation = await _mediator.Send(new AppointmentParticipations.Details.Query(dto.Id, currentUser.PersonId));
                if (participation != null)
                {
                    _mapper.Map(participation, dto);
                }
            }
            return listDto;
        }

        public async Task SetAppointmentParticipationPredictionAsync(SetMyProjectAppointmentPredictionDto setParticipationPredictionDto)
        {
            AppointmentParticipations.SetPrediction.Command command = _mapper
                .Map<AppointmentParticipations.SetPrediction.Command>(setParticipationPredictionDto);

            User currentUser = await _userAccessor.GetCurrentUserAsync();
            command.PersonId = currentUser.PersonId;

            await _mediator.Send(command);
        }
    }
}
