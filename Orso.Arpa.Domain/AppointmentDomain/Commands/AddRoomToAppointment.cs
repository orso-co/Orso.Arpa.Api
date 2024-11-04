using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class AddRoomToAppointment
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
                    .EntityExists<Command, Appointment>(arpaContext);

                RuleFor(d => d.RoomId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Room>(arpaContext)

                    .MustAsync(async (dto, roomId, cancellation) => !await arpaContext
                        .EntityExistsAsync<AppointmentRoom>(ar => ar.RoomId == roomId && ar.AppointmentId == dto.Id, cancellation))
                    .WithMessage("The room is already linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(
                IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Set<Appointment>().FindAsync([request.Id], cancellationToken);
                Room existingRoom = await _arpaContext.Set<Room>().FindAsync([request.RoomId], cancellationToken);

                var appointmentRoom = new AppointmentRoom(null, existingAppointment, existingRoom);

                await _arpaContext.Set<AppointmentRoom>().AddAsync(appointmentRoom);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(AppointmentRoom));
            }
        }
    }
}
