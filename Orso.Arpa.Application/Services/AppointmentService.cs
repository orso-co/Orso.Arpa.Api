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
    }
}
