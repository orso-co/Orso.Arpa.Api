using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.AppointmentParticipations
{
    public static class Details
    {
        public class Query : IRequest<AppointmentParticipation>
        {
            public Query(Guid appointmentId, Guid personId)
            {
                AppointmentId = appointmentId;
                PersonId = personId;
            }

            public Guid AppointmentId { get; }
            public Guid PersonId { get; }
        }

        public class Handler : IRequestHandler<Query, AppointmentParticipation>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<AppointmentParticipation> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _arpaContext.AppointmentParticipations
                    .SingleOrDefaultAsync(ap => ap.AppointmentId == request.AppointmentId && ap.PersonId == request.PersonId, cancellationToken);
            }
        }
    }
}
