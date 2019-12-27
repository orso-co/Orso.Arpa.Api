using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
{
    public static class RemoveRegister
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid registerId)
            {
                Id = id;
                RegisterId = registerId;
            }

            public Guid Id { get; private set; }
            public Guid RegisterId { get; private set; }
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

                RegisterAppointment registerToRemove = existingAppointment.RegisterAppointments.FirstOrDefault(r => r.RegisterId == request.RegisterId);

                existingAppointment.RegisterAppointments.Remove(registerToRemove);

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
