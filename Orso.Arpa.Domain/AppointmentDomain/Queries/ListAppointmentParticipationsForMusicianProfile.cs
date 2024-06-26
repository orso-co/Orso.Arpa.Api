using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Queries
{
    public static class ListAppointmentParticipationsForMusicianProfile
    {
        public class Query : IRequest<IEnumerable<AppointmentParticipation>>
        {
            public Guid MusicianProfileId { get; set; }
            public Guid? ProjectId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Query, MusicianProfile>(arpaContext);

                _ = RuleFor(c => c.ProjectId)
                    .EntityExists<Query, Project>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<AppointmentParticipation>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IEnumerable<AppointmentParticipation>> Handle(Query request, CancellationToken cancellationToken)
            {
                Guid personId = (await _arpaContext.FindAsync<MusicianProfile>([request.MusicianProfileId], cancellationToken)).PersonId;

                IQueryable<AppointmentParticipation> appointmentParticipations = _arpaContext.AppointmentParticipations
                    .Where(ap => ap.PersonId.Equals(personId));

                if (await appointmentParticipations.AnyAsync(cancellationToken) && request.StartTime.HasValue)
                {
                    var normalizedTime = new DateTime(request.StartTime.Value.Ticks, DateTimeKind.Unspecified);
                    appointmentParticipations = appointmentParticipations.Where(ap => ap.Appointment.StartTime.Equals(normalizedTime));
                }

                if (await appointmentParticipations.AnyAsync(cancellationToken) && request.EndTime.HasValue)
                {
                    var normalizedTime = new DateTime(request.EndTime.Value.Ticks, DateTimeKind.Unspecified);
                    appointmentParticipations = appointmentParticipations.Where(ap => ap.Appointment.EndTime.Equals(normalizedTime));
                }

                if (await appointmentParticipations.AnyAsync(cancellationToken) && request.ProjectId.HasValue)
                {
                    appointmentParticipations = appointmentParticipations.Where(ap =>
                        ap.Appointment.ProjectAppointments.Any(pa => pa.ProjectId.Equals(request.ProjectId)));
                }

                return await appointmentParticipations.ToListAsync(cancellationToken);
            }
        }
    }
}
