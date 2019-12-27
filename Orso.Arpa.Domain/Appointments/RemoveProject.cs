using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
{
    public static class RemoveProject
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid projectId)
            {
                Id = id;
                ProjectId = projectId;
            }

            public Guid Id { get; private set; }
            public Guid ProjectId { get; private set; }
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

                ProjectAppointment projectAppointment = existingAppointment.ProjectAppointments.FirstOrDefault(r => r.ProjectId == request.ProjectId);

                existingAppointment.ProjectAppointments.Remove(projectAppointment);

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
