using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
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
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext, nameof(Command.Id));

                RuleFor(d => d.RoomId)
                    .EntityExists<Command, Room>(arpaContext, nameof(Command.RoomId))

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
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellationToken);
                Room existingRoom = await _arpaContext.Rooms.FindAsync(new object[] { request.RoomId }, cancellationToken);

                var appointmentRoom = new AppointmentRoom(null, existingAppointment, existingRoom);

                _arpaContext.AppointmentRooms.Add(appointmentRoom);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem creating appointment room");
            }
        }
    }
}
