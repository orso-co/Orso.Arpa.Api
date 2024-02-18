using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.AppointmentDomain.Queries
{
    public static class GetAppointmentParticipation
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
                return await _arpaContext
                    .Set<AppointmentParticipation>()
                    .SingleOrDefaultAsync(ap => ap.AppointmentId == request.AppointmentId && ap.PersonId == request.PersonId, cancellationToken);
            }
        }
    }
}
