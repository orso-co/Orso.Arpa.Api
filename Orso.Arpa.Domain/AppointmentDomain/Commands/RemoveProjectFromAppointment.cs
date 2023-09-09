using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class RemoveProjectFromAppointment
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
                ProjectAppointment projectAppointment = await _arpaContext.ProjectAppointments
                    .FirstOrDefaultAsync(pa => pa.ProjectId == request.ProjectId && pa.AppointmentId == request.Id, cancellationToken);

                _arpaContext.ProjectAppointments.Remove(projectAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(ProjectAppointment));
            }
        }
    }
}
