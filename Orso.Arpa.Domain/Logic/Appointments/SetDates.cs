using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class SetDates
    {
        public class Command : IRequest<Appointment>
        {
            public Guid Id { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext, IMapper mapper)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext);

                RuleFor(d => d.EndTime)
                    .MustAsync(async (request, _, cancellation) =>
                    {
                        Appointment existingAppointment = await arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellation);
                        existingAppointment.Update(request);
                        return existingAppointment?.EndTime >= existingAppointment?.StartTime;
                    })
                    .WithMessage("EndTime must be greater than StartTime");
            }
        }

        public class Handler : IRequestHandler<Command, Appointment>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(
                IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Appointment> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellationToken);

                existingAppointment.Update(request);

                EntityEntry<Appointment> changedAppointment = _arpaContext.Appointments.Update(existingAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return changedAppointment?.Entity;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}
