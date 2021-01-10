using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
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

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid ProjectId { get; private set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                
                RuleFor(d => d.ProjectId)
                    .MustAsync(async (dto, projectId, cancellation) => await arpaContext.ProjectAppointments
                        .AnyAsync(pa => pa.AppointmentId == dto.Id && pa.ProjectId == projectId, cancellation))
                    .WithMessage("The project is not linked to the appointment");
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
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(request.Id);

                ProjectAppointment projectAppointment = existingAppointment.ProjectAppointments
                    .FirstOrDefault(r => r.ProjectId == request.ProjectId);

                existingAppointment.ProjectAppointments.Remove(projectAppointment);

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
