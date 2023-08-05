using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.GenericHandlers;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Venues
{
    public class DeleteNotification : IEntityDeleteNotification<Venue>
    {
        public Guid Id { get; set; }
    }

    public class Handler : INotificationHandler<DeleteNotification>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task Handle(DeleteNotification notification, CancellationToken cancellationToken)
        {
            IList<Appointment> appointmentsWithVenue = await _arpaContext.Set<Appointment>()
                .Where(a => notification.Id.Equals(a.VenueId))
                .ToListAsync(cancellationToken);

            foreach (Appointment appointment in appointmentsWithVenue)
            {
                appointment.ClearVenue();
                _arpaContext.Entry(appointment)?.CurrentValues?.SetValues(appointment);
            }

            if ((await _arpaContext.SaveChangesAsync(cancellationToken)) < appointmentsWithVenue.Count)
            {
                throw new AffectedRowCountMismatchException(nameof(Appointment));
            }
        }
    }
}
