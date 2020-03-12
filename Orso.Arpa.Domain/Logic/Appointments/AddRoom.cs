using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class AddRoom
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid roomId)
            {
                Id = id;
                RoomId = roomId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid RoomId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, AppointmentRoom>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(d => d.Id)
                    .MustAsync(async (id, cancellation) => await arpaContext.Appointments
                        .AnyAsync(a => a.Id == id, cancellation))
                    .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));

                RuleFor(d => d.RoomId)
                    .MustAsync(async (roomId, cancellation) => await arpaContext.Rooms
                        .AnyAsync(a => a.Id == roomId, cancellation))
                    .OnFailure(dto => throw new RestException("Room not found", HttpStatusCode.NotFound, new { Room = "Not found" }))

                    .MustAsync(async (dto, roomId, cancellation) => !(await arpaContext.AppointmentRooms
                        .AnyAsync(ar => ar.RoomId == roomId && ar.AppointmentId == dto.Id, cancellation)))
                    .WithMessage("The room is already linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(
                IArpaContext arpaContext,
                IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(request.Id);

                existingAppointment.AppointmentRooms.Add(_mapper.Map<AppointmentRoom>(request));

                _arpaContext.Appointments.Update(existingAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}
