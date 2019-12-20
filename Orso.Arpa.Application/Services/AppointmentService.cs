using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Appointments;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AppointmentService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task AddProjectAsync(Guid id, Guid projectId)
        {
            var command = new AddProject.Command(id, projectId);
            await _mediator.Send(command);
        }

        public async Task AddRegisterAsync(Guid id, Guid registerId)
        {
            var command = new AddRegister.Command(id, registerId);
            await _mediator.Send(command);
        }

        public async Task AddRoomAsync(Guid id, Guid roomId)
        {
            var command = new AddRoom.Command(id, roomId);
            await _mediator.Send(command);
        }

        public async Task<AppointmentDto> CreateAsync(AppointmentCreateDto appointmentCreateDto)
        {
            Create.Command command = _mapper.Map<Create.Command>(appointmentCreateDto);
            Appointment createdAppointment = await _mediator.Send(command);
            return _mapper.Map<AppointmentDto>(createdAppointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAsync(DateTime? date, DateRange range)
        {
            date ??= DateTime.Today;

            IImmutableList<Appointment> appointments = await _mediator.Send(new List.Query
            {
                StartTime = DateHelper.GetStartTime(date.Value, range),
                EndTime = DateHelper.GetEndTime(date.Value, range)
            });
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto> GetAsync(Guid id)
        {
            Appointment appointment = await _mediator.Send(new Details.Query { Id = id });
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task ModifyAsync(AppointmentModifyDto appointmentModifyDto)
        {
            Modify.Command command = _mapper.Map<Modify.Command>(appointmentModifyDto);
            await _mediator.Send(command);
        }

        public async Task RemoveProjectAsync(Guid id, Guid projectId)
        {
            var command = new RemoveProject.Command(id, projectId);
            await _mediator.Send(command);
        }

        public async Task RemoveRegisterAsync(Guid id, Guid registerId)
        {
            var command = new RemoveRegister.Command(id, registerId);
            await _mediator.Send(command);
        }

        public async Task RemoveRoomAsync(Guid id, Guid roomId)
        {
            var command = new RemoveRoom.Command(id, roomId);
            await _mediator.Send(command);
        }
    }
}
