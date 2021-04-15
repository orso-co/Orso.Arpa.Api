using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class RemoveRoom
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

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.RoomId)
                    .MustAsync(async (dto, roomId, cancellation) => await arpaContext.AppointmentRooms
                        .AnyAsync(ar => ar.RoomId == roomId && ar.AppointmentId == dto.Id, cancellation))
                    .WithMessage("The room is not linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                AppointmentRoom roomToRemove = await _arpaContext.AppointmentRooms
                                    .FirstOrDefaultAsync(ar => ar.RoomId == request.RoomId && ar.AppointmentId == request.Id, cancellationToken);

                _arpaContext.AppointmentRooms.Remove(roomToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem removing appointment room");
            }
        }
    }
}
