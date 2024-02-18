using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Commands
{
    public static class SetVenue
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid venueId)
            {
                Id = id;
                VenueId = venueId;
            }

            public Command()
            {
            }

            public Guid Id { get; private set; }
            public Guid VenueId { get; private set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext);

                RuleFor(d => d.VenueId)
                    .EntityExists<Command, Venue>(arpaContext);
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
                Appointment existingAppointment = await _arpaContext.Set<Appointment>().FindAsync(new object[] { request.Id }, cancellationToken);

                existingAppointment.Update(request);

                _arpaContext.Set<Appointment>().Update(existingAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(Appointment));
            }
        }
    }
}
