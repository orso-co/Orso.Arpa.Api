using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class RemoveRoomFromAppointment
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
                _ = RuleFor(d => d.RoomId)
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

                _ = _arpaContext.AppointmentRooms.Remove(roomToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(AppointmentRoom));
            }
        }
    }
}
