using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.AppointmentParticipations
{
    public static class GetForMusicianProfile
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
                Guid personId = (await _arpaContext.FindAsync<MusicianProfile>(new object[] { request.MusicianProfileId }, cancellationToken)).PersonId;

                IQueryable<AppointmentParticipation> appointmentParticipations = _arpaContext.AppointmentParticipations
                    .Where(ap => ap.PersonId.Equals(personId));

                if (appointmentParticipations.Any() && request.StartTime.HasValue)
                {
                    var normalizedTime = new DateTime(request.StartTime.Value.Ticks, DateTimeKind.Unspecified);
                    appointmentParticipations = appointmentParticipations.Where(ap => ap.Appointment.StartTime.Equals(normalizedTime));
                }

                if (appointmentParticipations.Any() && request.EndTime.HasValue)
                {
                    var normalizedTime = new DateTime(request.EndTime.Value.Ticks, DateTimeKind.Unspecified);
                    appointmentParticipations = appointmentParticipations.Where(ap => ap.Appointment.EndTime.Equals(normalizedTime));
                }

                if (appointmentParticipations.Any() && request.ProjectId.HasValue)
                {
                    IQueryable<ProjectParticipation> projectParticipations = _arpaContext.ProjectParticipations
                        .Where(pp => pp.MusicianProfileId.Equals(request.MusicianProfileId) && pp.ProjectId.Equals(request.ProjectId.Value));

                    appointmentParticipations = appointmentParticipations.Where(ap => ap.Appointment.ProjectAppointments.Any(pa => pa.ProjectId.Equals(request.ProjectId)));
                }

                return await appointmentParticipations.ToListAsync(cancellationToken);
            }
        }
    }
}
