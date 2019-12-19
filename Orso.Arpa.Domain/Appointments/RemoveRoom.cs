using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
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

            public Guid Id { get; private set; }
            public Guid RoomId { get; private set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _repository.GetByIdAsync<Appointment>(request.Id);

                if (existingAppointment == null)
                {
                    throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" });
                }

                AppointmentRoom roomToRemove = existingAppointment.AppointmentRooms.FirstOrDefault(r => r.RoomId == request.RoomId);

                if (roomToRemove != null)
                {
                    existingAppointment.AppointmentRooms.Remove(roomToRemove);
                }

                _repository.Update(existingAppointment);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}
